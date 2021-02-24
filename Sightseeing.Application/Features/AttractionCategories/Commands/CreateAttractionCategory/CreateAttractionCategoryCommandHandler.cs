using AutoMapper;
using MediatR;
using Sightseeing.Application.Contracts.Persistence;
using Sightseeing.Application.Exceptions;
using Sightseeing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sightseeing.Application.Features.AttractionCategories.Commands.CreateAttractionCategory
{
    public class CreateAttractionCategoryCommandHandler : IRequestHandler<CreateAttractionCategoryCommand, AttractionCategoryDto>
    {
        private readonly IMapper _mapper;
        private readonly IAttractionCategoryRepository _categoryRepository;

        public CreateAttractionCategoryCommandHandler(IMapper mapper, IAttractionCategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<AttractionCategoryDto> Handle(CreateAttractionCategoryCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateAttractionCategoryCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                throw new ValidationException(validationResult);
            }

            var category = _mapper.Map<AttractionCategory>(request);

            category = await _categoryRepository.AddAsync(category);

            var categoryDto = _mapper.Map<AttractionCategoryDto>(category);

            return categoryDto;
        }
    }
}
