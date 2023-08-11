using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Exeptions
{
    public class EditValidationException:Exception
    {
        private string _property;

        
        public EditValidationException(string message, string property=null) : base(message)
        {
            this._property = property;
        }

        public string Property { get => _property; set => _property = value; }
    }
}
