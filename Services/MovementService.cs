using backend_gestorinv.Context;
using backend_gestorinv.DTOs.MovementDTO;
using backend_gestorinv.Services.IServices;
using backend_gestorinv.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace backend_gestorinv.Services
{
    public class MovementService : IMovementService
    {
        private readonly AppDbContext _context;

        public MovementService(AppDbContext context)
        {
            _context = context;
        }

        // Registrar el movimiento de inventario
        public async Task<int> CreateMovement(MovementCreateDTO movementDTO, List<DetailMovementDTO> details)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var movement = new MovimientoInventario
                {
                    usuario_id = movementDTO.usuario_id,
                    tipo_movimiento = movementDTO.tipo_movimiento,
                    fecha_registro = DateTime.Now
                };

                _context.Movimientos_Inventario.Add(movement);
                await _context.SaveChangesAsync();

                // Se registra los detalles del movimiento 
                await AddMovementDetails(movement.id_movimiento, movement.tipo_movimiento, details);

                // Se actualiza el stock del producto
                await UpdateStock(movement.tipo_movimiento, details);

                await transaction.CommitAsync();
                return movement.id_movimiento;

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception($"Error al registrar el movimiento: {ex.Message}");
            }
        }

        // Registrar los detalles del movimiento
        public async Task AddMovementDetails(int movementId, string movementType, List<DetailMovementDTO> details)
        {
            var movementDetails = new List<DetalleMovimiento>();

            foreach (var detail in details)
            {
                var product = await _context.Inventario.FindAsync(detail.producto_id);
                if (product == null)
                    throw new Exception($"El producto con ID {detail.producto_id} no existe.");

                var movementDetail = new DetalleMovimiento
                {
                    movimiento_id = movementId,
                    producto_id = detail.producto_id,
                    cantidad = detail.cantidad,
                    precio_unitario = product.precio_unitario,
                    total = (movementType == "Salida") ? detail.cantidad * product.precio_unitario : null
                };

                movementDetails.Add(movementDetail);
            }
            _context.Detalles_Movimiento.AddRange(movementDetails);
            await _context.SaveChangesAsync();
        }

        // Actualizar stock del producto
        public async Task UpdateStock(string movementType, List<DetailMovementDTO> details)
        {
            foreach(var detail in details)
            {
                var product = await _context.Inventario.FindAsync(detail.producto_id);
                if (product == null)
                    throw new Exception($"El producto con ID {detail.producto_id} no existe.");

                if(movementType == "Entrada")
                {
                    product.stock += detail.cantidad;
                }
                else if(movementType == "Salida")
                {
                    if (product.stock < detail.cantidad)
                        throw new Exception($"Stock insuficiente para el producto con ID {detail.producto_id}");

                    product.stock -= detail.cantidad;
                }
            }
            await _context.SaveChangesAsync();
        }

        // Obtener todos los movimientos
        public async Task<List<MovementGetDTO>> GetAllMovements()
        {
            var movements = await _context.Movimientos_Inventario
                .Include(m => m.usuario)
                .ToListAsync();

            return movements.Select(m => new MovementGetDTO
            {
                id_movimiento = m.id_movimiento,
                usuario_id = m.usuario_id,
                usuario_nombre = m.usuario.nombre,
                tipo_movimiento = m.tipo_movimiento,
                fecha_registro = m.fecha_registro
            }).ToList(); 
        }

        // Obtener los detalles del movimiento
        public async Task<MovementWithDetails> GetMovementDetails(int movementId)
        {
            var movement = await _context.Movimientos_Inventario
                .Include(m => m.detalles)
                    .ThenInclude(d => d.producto)
                .Include(m => m.usuario)
                .FirstOrDefaultAsync(m => m.id_movimiento == movementId);

            if (movement == null)
                return null;

            return new MovementWithDetails
            {
                id_movimiento = movement.id_movimiento,
                usuario_id = movement.usuario_id,
                usuario_nombre = movement.usuario.nombre,
                tipo_movimiento = movement.tipo_movimiento,
                fecha_registro = movement.fecha_registro,
                detalles = movement.detalles.Select(d => new DetailsDTO
                {
                    producto_id = d.producto_id,
                    producto_nombre = d.producto.producto, 
                    cantidad = d.cantidad,
                    precio_unitario = d.precio_unitario,
                    total = d.total
                }).ToList()
            };
        }

    }
}
