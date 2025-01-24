using Application.Core.Abstractions.Data;
using Application.Core.Extensions;
using AutoMapper;
using Domain.Core.Primitives.Maybe;
using Domain.Core.Primitives.Result;
using MediatR;
using static Domain.Core.Errors.DomainErrors;

namespace Application.Task.Command.Update
{
    public class UpdateUserTaskCommandHandler : IRequestHandler<UpdateUserTaskCommand, Result<TaskDto>>
    {
        private readonly IGenericRepository<Domain.Entities.User> _userRepository;
        private readonly IGenericRepository<Domain.Entities.Task> _taskRepository;
        private readonly IMapper _mapper;

        public UpdateUserTaskCommandHandler(
            IGenericRepository<Domain.Entities.User> userRepository,
            IGenericRepository<Domain.Entities.Task> taskRepository,
            IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<TaskDto>> Handle(UpdateUserTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //return await _userRepository.GetByIdAsync(request.UserId)
                //       .EnsureNotNullAsync(Database.InternalServerError)
                //       .EnsureExistsAsync(User.NotFound)
                //       .BindAsync(_ => _taskRepository.GetByIdAsync(request.TaskId))
                //       .EnsureNotNullAsync(Database.InternalServerError)
                //       .EnsureExistsAsync(TaskError.NotFound)
                //       .BindAsync(t => _taskRepository.UpdateAsync(t))
                //       .EnsureNotNullAsync(Database.InternalServerError)
                //       .EnsureExistsAsync(General.UnProcessableRequest)
                //       .UnwrapAsync(General.ServerError)
                //       .MapAsync<Domain.Entities.Task, TaskDto>(_mapper);
                return (Result<TaskDto>)Result<TaskDto>.Failure(
                    new Domain.Core.Primitives.Error(System.Net.HttpStatusCode.InternalServerError,"" )); 

            }
            catch (Exception ex)
            {

                return (Result<TaskDto>)Result<TaskDto>.Failure(
                    new Domain.Core.Primitives.Error(System.Net.HttpStatusCode.InternalServerError, ex.Message + ex.InnerException));
            }
        }
    }
}
