using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules
{
    public class MessageValidator : AbstractValidator<Message>
    {
        public MessageValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("İsim alanı boş bırakılamaz!");
            RuleFor(x => x.FullName).Length(3, 30).WithMessage("En az 3, en fazla 30 karakter girilmelidir!");

            RuleFor(x => x.Email).NotEmpty().WithMessage("E-Mail alanı boş bırakılamaz!");

            RuleFor(x => x.Content).NotEmpty().WithMessage("Mesaj alanı boş bırakılamaz!");
            RuleFor(x => x.Content).Length(10, 300).WithMessage("En az 10, en fazla 300 karakter girilmelidir!");
        }
    }
}
