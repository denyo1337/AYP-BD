using IdGen;

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
