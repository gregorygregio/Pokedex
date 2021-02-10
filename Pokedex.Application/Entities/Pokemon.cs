using System;
using System.Text;
using System.Collections.Generic;
using FluentValidation;

namespace Pokedex.Application.Entities
{
    public class Pokemon : BaseEntity<Pokemon>
    {
        public int pokedex_index { get; set; }

        public string name { get; set; }

        public int hp { get; set; }

        public int attack { get; set; }

        public int defense { get; set; }

        public int special_attack { get; set; }

        public int special_defense { get; set; }

        public int speed { get; set; }

        public int generation { get; set; }

        public int Total => hp + attack + defense + special_attack + special_defense + speed;

        public IEnumerable<PokemonType> Types { get; set; }

        public override bool IsValid()
        {
            ValidatePokedexIndex();
            ValidateName();
            ValidateHP();
            ValidateAttack();
            ValidateDefense();
            ValidateSpecialAttack();
            ValidateSpecialDefense();
            ValidateSpeed();
            ValidateGeneration();

            ValidationResult = Validate(this);

            return ValidationResult.IsValid; ;
        }

        public void ValidatePokedexIndex()
        {
            //pokedex_index -> um inteiro único, correspondente ao index do pokedex do pokemon
            RuleFor(p => p.pokedex_index);
        }

        public void ValidateName()
        {
            //name->varchar(26) ÚNICO com o nome do pokemon
            RuleFor(p => p.name)
                .NotEmpty()
                .WithMessage("A name must be informed.")
                .MaximumLength(26)
                .WithMessage("The name shouldn't have more than 26 characters");
        }

        public void ValidateHP()
        {
            //hp->inteiro correspondente à HP, com valor entre 1 e 255(inclusos)
            RuleFor(p => p.hp)
                .InclusiveBetween(1, 255)
                .WithMessage("The HP must be a value between 1 and 255");
        }

        public void ValidateAttack()
        {
            //attack -> inteiro correspondente aos pontos de ataque, valor entre 5 e 190 (inclusos)
            RuleFor(p => p.attack)
                .InclusiveBetween(5, 190)
                .WithMessage("The Attack must be a value between 5 and 190");
        }

        public void ValidateDefense()
        {
            //defense -> inteiro correspondente aos pontos de defesa, valor entre 5 e 230 (inclusos)
            RuleFor(p => p.defense)
                .InclusiveBetween(5, 230)
                .WithMessage("The Defense must be a value between 5 and 230");
        }

        public void ValidateSpecialAttack()
        {
            //special_attack -> inteiro correspondente aos pontos de sp.ataque, valor entre 10 e 194 (inclusos)
            RuleFor(p => p.special_attack)
                .InclusiveBetween(10, 194)
                .WithMessage("The Special Attack must be a value between 10 and 194");
        }

        public void ValidateSpecialDefense()
        {
            //special_defense -> inteiro correspondente aos pontos de sp. defesa, valor entre 20 e 230 (inclusos)
            RuleFor(p => p.special_defense)
                .InclusiveBetween(20, 230)
                .WithMessage("The Special Defense must be a value between 20 and 230");
        }

        public void ValidateSpeed()
        {
            //speed -> inteiro correspondente aos pontos de velocidade, valor entre 5 e 180 (inclusos)
            RuleFor(p => p.speed)
                .InclusiveBetween(5, 180)
                .WithMessage("The Speed must be a value between 5 and 180");
        }

        public void ValidateGeneration()
        {
            //generation->inteiro correspondente à geração do pokemon, valor entre 1 e 6(inclusos)
            RuleFor(p => p.generation)
                .InclusiveBetween(1, 6)
                .WithMessage("The Generation must be a value between 1 and 6");
        }
    }
}
