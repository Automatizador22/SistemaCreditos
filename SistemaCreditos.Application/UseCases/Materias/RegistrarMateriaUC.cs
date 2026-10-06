using SistemaCreditos.Application.DTOs.Materias;
using SistemaCreditos.Application.Exceptions;
using SistemaCreditos.Application.Interfaces.Persistence;
using SistemaCreditos.Domain.Entities;

namespace SistemaCreditos.Application.UseCases.Materias
{
    public class RegistrarMateriaUC
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegistrarMateriaUC(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RegistrarMateriaResponseDto> EjecutarAsync(RegistrarMateriaRequestDto dto)
        {
            string? nombreProfesor = null;

            if (dto.IdProfesor.HasValue)
            {
                var materiasDelProfesor = await _unitOfWork.Materias.GetMateriasPorProfesorAsync(dto.IdProfesor.Value);
                if (materiasDelProfesor.Count >= 2)
                {
                    throw new ValidationException("Este profesor ya tiene el límite máximo de 2 materias asignadas.");
                }
                var profesor = await _unitOfWork.Profesores.GetByIdAsync(dto.IdProfesor.Value);
                if (profesor == null) throw new ValidationException("El profesor asignado no existe.");

                var usuarioProfesor = await _unitOfWork.Usuarios.GetByIdAsync(profesor.IdUsuario);
                nombreProfesor = usuarioProfesor != null
                    ? usuarioProfesor.ObtenerNombreCompleto()
                    : "Profesor Sin Nombre";
            }

            var nuevaMateria = new Materia
            {
                CodMateria = Guid.NewGuid(),
                NombreMateria = dto.NombreMateria,
                Creditos = 3,
                IdProfesor = dto.IdProfesor,
                FechaRegistro = DateTime.UtcNow
            };

            await _unitOfWork.Materias.AddAsync(nuevaMateria);
            await _unitOfWork.SaveChangesAsync();

            return new RegistrarMateriaResponseDto
            {
                IdMateria = nuevaMateria.IdMateria,
                CodMateria = nuevaMateria.CodMateria.ToString(),
                NombreMateria = nuevaMateria.NombreMateria,
                Creditos = nuevaMateria.Creditos,
                NombreProfesor = nombreProfesor,
                FechaRegistro = nuevaMateria.FechaRegistro,
                TotalEstudiantesInscritos = 0
            };
        }
    }
}