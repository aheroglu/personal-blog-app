using Entity.Concrete;
using FluentValidation;

namespace Business.ValidationRules
{
    public class PostCategoryValidator : AbstractValidator<PostCategory>
    {
        public PostCategoryValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Kategori adı alanı boş bırakılamaz!");
            RuleFor(x => x.Name).Length(3, 20).WithMessage("En az 3, en fazla 20 karakter girilmelidir!");
        }
    }
}
