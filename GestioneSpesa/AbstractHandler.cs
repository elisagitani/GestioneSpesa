using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneSpesa
{
    
        public abstract class AbstractHandler : IHandler
        {
            private IHandler _nextHandler;

            public virtual string Handle(Spesa request)
            {
                if (_nextHandler != null)
                    return _nextHandler.Handle(request);
                else
                    return null;
            }

            public IHandler SetNext(IHandler next)
            {
                _nextHandler = next;

                return next;
            }
        }

        public interface IHandler
        {
            IHandler SetNext(IHandler next);

            string Handle(Spesa request);
        }
    }

