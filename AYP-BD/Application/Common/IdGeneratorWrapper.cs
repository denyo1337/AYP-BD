﻿using IdGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public class IdGeneratorWrapper : IEntityGenerator
    {
        private readonly IdGenerator _generator;
        public IdGeneratorWrapper()
        {
            _generator = new IdGenerator(0);
        }
        public long Generate()
        {
            return _generator.CreateId();
        }
    }
}
