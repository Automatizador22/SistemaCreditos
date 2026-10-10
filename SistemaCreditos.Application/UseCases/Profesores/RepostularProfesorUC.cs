
using SistemaCreditos.Application.Exceptions;
using SistemaCreditos.Application.Interfaces.Persistence;

namespace SistemaCreditos.Application.UseCases.Profesores
{
    public class RepostularProfesorUC
    {
        private readonly IUnitOfWork _unitOfWork;

        public RepostularProfesorUC(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> EjecutarAsync(int IdUsuario)
        {
            var profesor = await _unitOfWork.Profesores.GetByIdUsuarioAsync(IdUsuario);
            if (profesor == null) throw new ValidationException("No se encontró un perfil de profesor asociado a tu cuenta.");

            profesor.Repostular();

            _unitOfWork.Profesores.Update(profesor);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
