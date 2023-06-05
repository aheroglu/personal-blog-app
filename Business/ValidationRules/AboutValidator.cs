using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules
{
    public class AboutValidator : AbstractValidator<About>
    {
        public AboutValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Başlık alanı boş bırakılamaz!");
            RuleFor(x => x.Title).Length(5, 100).WithMessage("En az 5, en fazla 100 karakter girilmelidir!");

            RuleFor(x => x.Content).NotEmpty().WithMessage("İçerik alanı boş bırakılamaz!");
            RuleFor(x => x.Content).Length(10, 5000).WithMessage("En az 10, en fazla 5000 karakter girilmelidir!");
        }
    }
}
