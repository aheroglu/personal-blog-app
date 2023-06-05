using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules
{
    public class PostValidator : AbstractValidator<Post>
    {
        public PostValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Başlık alanı boş bırakılamaz!");
            RuleFor(x => x.Title).Length(3, 100).WithMessage("En az 3, en fazla 100 karakter girilmelidir!");

            RuleFor(x => x.Content).NotEmpty().WithMessage("İçerik alanı boş bırakılamaz!");
        }
    }
}
