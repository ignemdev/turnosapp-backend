using Queues.Domain.Constants;
using Queues.Domain.Enumerables;

namespace Queues.Application.Person.Extensions;
public static class PersonExtensions
{
    public static PreferenceLevel GetPreferenceLevel(this Domain.Entities.Person person)
    {
        if (person.Pregnant)
            return PreferenceLevel.First;

        if (person.IsInSecondPreferenceLevel())
            return PreferenceLevel.Second;

        return PreferenceLevel.Third;
    }

    public static double GetBMI(this Domain.Entities.Person person)
    {
        var bmi = person.Weight / Math.Pow(person.Height, 2);

        return bmi;
    }

    public static int GetAge(this Domain.Entities.Person person)
    {
        var difference = DateTime.Now - person.Birthdate;
        var age = (int)(difference.TotalDays / GenericConstants.DAYS_IN_A_YEAR);
        return age;
    }

    private static bool IsInSecondPreferenceLevel(this Domain.Entities.Person person)
    {
        var age = person.GetAge();
        var bmi = person.GetBMI();

        var isSecondLevelPreference = person.HealthIssues ||
            age >= GenericConstants.MINIMUM_PREFERENCE_AGE ||
            bmi >= GenericConstants.MINIMUM_PREFERENCE_BMI;

        return isSecondLevelPreference;
    }
}

