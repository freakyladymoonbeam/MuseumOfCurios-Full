using MuseumOfCurios.Systems;

namespace MuseumOfCurios.Characters.PlayerCharacter
{
    public class Protag
    {
        // Basic Identity Properties
        public string PlayerName { get; set; }
        public BodyType PlayerBody { get; set; }
        public Pronouns PlayerPronouns { get; set; }  // Using the Pronouns class for flexible pronoun management
        public RelationshipStatus RelationshipStatus { get; set; }  // New property for relationship status

        // Curator-specific Properties
        public int CuratorRank { get; set; }  // e.g., 1 = Novice, 5 = Master
        // Additional properties can be added later as needed

        // Constructor
        public Protag(string playerName, BodyType playerBody, Pronouns playerPronouns, int curatorRank)
        {
            PlayerName = playerName;
            PlayerBody = playerBody;
            PlayerPronouns = playerPronouns;
            CuratorRank = curatorRank;
            RelationshipStatus = RelationshipStatus.Single;  // Default relationship status
        }

        // Allow changing pronouns anytime
        public void ChangePronouns(Pronouns newPronouns)
        {
            PlayerPronouns = newPronouns;
        }

        // Method to update relationship status
        public void UpdateRelationshipStatus(RelationshipStatus newStatus)
        {
            RelationshipStatus = newStatus;
        }
    }

    // Enum for Gender
    public enum BodyType
    {
        Masculine,
        Feminine,
        Androgynous
    }

    // Enum for Relationship Status
    public enum RelationshipStatus
    {
        Single,
        Dating,
        Married
    }
}