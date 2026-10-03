// Requirements
// You need to create a game that consists of asking the player what's the result of a math question (i.e. 9 x 9 = ?), collecting the input and adding a point in case of a correct answer.
// A game needs to have at least 5 questions.
// The divisions should result on INTEGERS ONLY and dividends should go from 0 to 100. Example: Your app shouldn't present the division 7/2 to the user, since it doesn't result in an integer.
// Users should be presented with a menu to choose an operation
// You should record previous games in a List and there should be an option in the menu for the user to visualize a history of previous games.
// You don't need to record results on a database. Once the program is closed the results will be deleted.

//Variables
Random random = new Random();
List<string> GameHistory = new List<string>();

void GenerateDivisionQuestion()
{
    int firstRandomNumber = random.Next(1, 101);
    int secondRandomNumber;
    do
    {
        secondRandomNumber = random.Next(1, 101);
    }
    while (firstRandomNumber % secondRandomNumber != 0);
    AskQuestion(firstRandomNumber, secondRandomNumber, OperationType.Division);
}

void GenerateQuestion(OperationType type)
{
    if (type == OperationType.Division)
    {
        GenerateDivisionQuestion();
        return;
    }

    int firstRandomNumber = random.Next(1, 101);
    int secondRandomNumber = random.Next(1, 101);
    AskQuestion(firstRandomNumber, secondRandomNumber, type);
}

int ReadAnswer()
{
    return Convert.ToInt32(Console.ReadLine());
}

int correctAnswer(int firstRandomNumber, int secondRandomNumber, OperationType type)
{
    return type switch
    {
        OperationType.Addition => firstRandomNumber + secondRandomNumber,
        OperationType.Subtraction => firstRandomNumber - secondRandomNumber,
        OperationType.Multiplication => firstRandomNumber * secondRandomNumber,
        OperationType.Division => firstRandomNumber / secondRandomNumber,
        _ => throw new ArgumentException("Invalid operation type.")
    };
}

void AskQuestion(int firstRandomNumber, int secondRandomNumber, OperationType type)
{
    switch (type)
    {
        case OperationType.Addition:
            Console.WriteLine("What is " + firstRandomNumber + " + " + secondRandomNumber + "?");
            int answer = ReadAnswer();

            if (answer == correctAnswer(firstRandomNumber, secondRandomNumber, type))
            {
                GameHistoryAddCorrect(firstRandomNumber,secondRandomNumber,'+', answer);
                Console.WriteLine("Correct!");
            }
            else
            {
                Console.WriteLine("Incorrect. The correct answer is " + correctAnswer(firstRandomNumber, secondRandomNumber, type));
                GameHistoryAddIncorrect(firstRandomNumber, secondRandomNumber, '+', answer, correctAnswer(firstRandomNumber, secondRandomNumber, type));
            }
            break;

        case OperationType.Subtraction:
            Console.WriteLine("What is " + firstRandomNumber + " - " + secondRandomNumber + "?");
            int subtractionAnswer = ReadAnswer();
            if (subtractionAnswer == correctAnswer(firstRandomNumber, secondRandomNumber, OperationType.Subtraction))
            {
                GameHistoryAddCorrect(firstRandomNumber, secondRandomNumber, '-', subtractionAnswer);
                Console.WriteLine("Correct!");
            }
            else
            {
                Console.WriteLine("Incorrect. The correct answer is " + correctAnswer(firstRandomNumber, secondRandomNumber, OperationType.Subtraction));
                GameHistoryAddIncorrect(firstRandomNumber, secondRandomNumber, '-', subtractionAnswer, correctAnswer(firstRandomNumber, secondRandomNumber, OperationType.Subtraction));
            }
            break;

        case OperationType.Multiplication:
            Console.WriteLine("What is " + firstRandomNumber + " * " + secondRandomNumber + "?");
            int multiplicationAnswer = ReadAnswer();
            if (multiplicationAnswer == correctAnswer(firstRandomNumber, secondRandomNumber, OperationType.Multiplication))
            {
                GameHistoryAddCorrect(firstRandomNumber, secondRandomNumber, '*', multiplicationAnswer);
                Console.WriteLine("Correct!");
            }
            else
            {
                GameHistoryAddIncorrect(firstRandomNumber, secondRandomNumber, '*', multiplicationAnswer, correctAnswer(firstRandomNumber, secondRandomNumber, OperationType.Multiplication));
                Console.WriteLine("Incorrect. The correct answer is " + correctAnswer(firstRandomNumber, secondRandomNumber, OperationType.Multiplication));
            }
            break;

        case OperationType.Division:
            Console.WriteLine("What is " + firstRandomNumber + " / " + secondRandomNumber + "?");
            int divisionAnswer = ReadAnswer();
            if (divisionAnswer == correctAnswer(firstRandomNumber, secondRandomNumber, OperationType.Division))
            {
                GameHistoryAddCorrect(firstRandomNumber, secondRandomNumber, '/', divisionAnswer);
                Console.WriteLine("Correct!");
            }
            else
            {
                GameHistoryAddIncorrect(firstRandomNumber, secondRandomNumber, '/', divisionAnswer, correctAnswer(firstRandomNumber, secondRandomNumber, OperationType.Division));
                Console.WriteLine("Incorrect. The correct answer is " + correctAnswer(firstRandomNumber, secondRandomNumber, OperationType.Division));
            }
            break;

        default:
            throw new ArgumentException("Invalid operation type.");
    }
}

void GameLoop(OperationType type)
{
    for (int i = 0; i < 5; i++)
    {
        GenerateQuestion(type);
    }
}

void DisplayGameHistory()
{
    Console.WriteLine("Game History:");
    foreach (var entry in GameHistory)
    {
        Console.WriteLine(entry);
    }
}

void GameHistoryAddCorrect(int firstNumber, int secondNumber,char operation, int answer)
{
    GameHistory.Add(firstNumber + " " + operation + " " + secondNumber + " = " + answer + " (Correct)");
}

void GameHistoryAddIncorrect(int firstNumber, int secondNumber, char operation, int answer, int correctAnswer)
{
    GameHistory.Add(firstNumber + " " + operation + " " + secondNumber + " = " + answer + " (Incorrect, correct answer: " + correctAnswer + ")");
}

void EndGame()
{

    Console.WriteLine("Game Over!");
    Console.WriteLine("Do you want to see your game history? (Y/N)");
    string? response = Console.ReadLine();
    if (response == "Y" || response == "y")
    {
        DisplayGameHistory();
    }
}

//Main program
var options = new GameOptions {};
options.PointsPerCorrectAnswer = 0;
Console.WriteLine("Welcome to the Math Game!");
Console.WriteLine("You will be asked to solve a series of addition problems.");
Console.WriteLine("You will earn points for each correct answer and lose lives for each incorrect answer.");
Console.WriteLine("Let's get started!");

Console.WriteLine("================================");

Console.WriteLine("Pick type of game you want to play:");
Console.WriteLine("A. Addition");
Console.WriteLine("B. Subtraction");
Console.WriteLine("C. Multiplication");
Console.WriteLine("D. Division");

char gameType = Console.ReadKey().KeyChar;

OperationType type = gameType switch
{
    'A' or 'a' => OperationType.Addition,
    'B' or 'b' => OperationType.Subtraction,
    'C' or 'c' => OperationType.Multiplication,
    'D' or 'd' => OperationType.Division,
    _ => throw new ArgumentException("Invalid game type selected.")
};
Console.WriteLine();
GameLoop(type);
EndGame();

//Options for the game
public sealed class GameOptions
{
    public int PointsPerCorrectAnswer { get; set; }
    public bool ShowGameHistory { get; set; } = true;
}

public enum OperationType
{
    Addition,
    Subtraction,
    Multiplication,
    Division
}
