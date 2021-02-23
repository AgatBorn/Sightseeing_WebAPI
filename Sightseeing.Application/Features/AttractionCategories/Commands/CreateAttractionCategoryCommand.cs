﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sightseeing.Application.Features.AttractionCategories.Commands
{
    public class CreateAttractionCategoryCommand : IRequest<AttractionCategoryDto>
    {
        public string Name { get; set; }
    }
}
