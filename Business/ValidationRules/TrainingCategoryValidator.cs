using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules
{
    public class TrainingCategoryValidator : AbstractValidator<TrainingCategory>
    {
        public TrainingCategoryValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Kategori adı alanı boş bırakılamaz!");
            RuleFor(x => x.Name).Length(3, 20).WithMessage("En az 3, en fazla 20 karakter girilmelidir!");
        }
    }
}
