using Dictionary.Api.Domain.Entities;

namespace Dictionary.Api.UseCases.Words;

public class WordGender
{
    public static WordGender? Create(Word word)
    {
        if (word is { GenderMasculine: false, GenderFeminine: false, GenderNeuter: false })
        {
            return null;
        }

        return new WordGender
        {
            Masculine = word.GenderMasculine,
            Feminine = word.GenderFeminine,
            Neuter = word.GenderNeuter
        };
    }

    public bool Masculine { get; set; }

    public bool Feminine { get; set; }

    public bool Neuter { get; set; }
}
