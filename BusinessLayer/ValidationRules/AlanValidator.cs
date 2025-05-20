using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;
using FluentValidation;
using FluentValidation.Validators;

namespace BusinessLayer.ValidationRules
{
    public class AlanValidator : AbstractValidator<Alan>
    {
        //Başka validatorlar da eklenmeli hem buna hem de diger entityler icin
        public AlanValidator() {
            RuleFor(x => x.AlanName).NotEmpty().WithMessage("AlanName bos");
            RuleFor(x => x.AlanName).Matches(@"^[a-z]+$").WithMessage("Alan adı sadee küçük harflerden oluşmalı.");
            


        }
    }
}
