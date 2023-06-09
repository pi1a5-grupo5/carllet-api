﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICourseService
    {
        Task<Course> Register(Course course);

        Task<List<Course>> GetByUserId(Guid driver);
    }
}
