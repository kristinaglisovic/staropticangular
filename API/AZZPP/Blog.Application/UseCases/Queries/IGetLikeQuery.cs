﻿using Blog.Application.UseCases.DTO.Like;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.UseCases.Queries
{
    public interface IGetLikeQuery: IQuery<int, LikeDTO>
    {
    }
}
