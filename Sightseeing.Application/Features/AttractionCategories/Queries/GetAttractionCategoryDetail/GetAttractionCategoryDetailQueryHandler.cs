using AutoMapper;
using MediatR;
using Sightseeing.Application.Contracts.Persistence;
using Sightseeing.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sightseeing.Application.Features.AttractionCategories.Queries.GetAttractionCategoryDetail
{
    public class GetAttractionCategoryDetailQueryHandler : IRequestHandler<GetAttractionCategoryDetailQuery, AttractionCategoryVm>
    {
        private readonly IMapper _mapper;
        private readonly IAttractionCategoryRepository _categoryRepository;

        public GetAttractionCategoryDetailQueryHandler(IMapper mapper, IAttractionCategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<AttractionCategoryVm> Handle(GetAttractionCategoryDetailQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetAttractionCategoryDetailQueryValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                throw new ValidationException(validationResult);
            }

            var attractionCategory = await _categoryRepository.GetByIdWithRelatedDataAsync(request.Id);

            if (attractionCategory == null)
            {
                throw new ApplicationException($"Attraction category {request.Id} not found");
            }

            var attractionCategoryDetailVm = _mapper.Map<AttractionCategoryVm>(attractionCategory);

            return attractionCategoryDetailVm;
        }
    }
}
