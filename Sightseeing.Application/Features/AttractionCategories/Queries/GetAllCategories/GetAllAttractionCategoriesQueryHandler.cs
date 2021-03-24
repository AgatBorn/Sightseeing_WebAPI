using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Sightseeing.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sightseeing.Application.Features.AttractionCategories.Queries.GetAllCategories
{
    public class GetAllAttractionCategoriesQueryHandler : IRequestHandler<GetAllAttractionCategoriesQuery, IList<AttractionCategoryListVm>>
    {
        private readonly IMapper _mapper;
        private readonly IAttractionCategoryRepository _categoryRepository;

        public GetAllAttractionCategoriesQueryHandler(IMapper mapper, IAttractionCategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<IList<AttractionCategoryListVm>> Handle(GetAllAttractionCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetAllAsync();

            var categoriesVm = _mapper.Map<List<AttractionCategoryListVm>>(categories);

            return categoriesVm;
        }
    }
}
