bool playerInterest = true;
int[] globalStats = new int[] { 0, 0, 0, 0 }; // player 1 wins, player 2 wins, draws, total games 
while (playerInterest)
{
    Console.Clear();
    int playerMoveCounter = 0;
    string[,] board = new string[,]
    {
        {"1","2","3" },
        {"4","5","6" },
        {"7","8","9" }
    };
    printBoard(board, globalStats);

    string winner = "";
    bool activeGame = true;
    while (activeGame)
    {
        playerMoveCounter++;
        makeMove(board, playerMoveCounter);
        Console.Clear();
        printBoard(board, globalStats);
        winner = winChecker(board);
        if (winner != "")
        {
            activeGame = false;
        }
    }
    messageAndStats(board, winner, globalStats);
    if (!playAgain()) break;
}


static void printBoard(string[,] board, int[] globalStats)
{
    Console.WriteLine($"Player 1 Wins: {globalStats[0]} Draws: {globalStats[2]}");
    Console.WriteLine($"Player 2 Wins: {globalStats[1]} Games: {globalStats[3]}");
    Console.WriteLine("       |       |       ");
    Console.WriteLine($"   {board[0, 0]}   |   {board[0, 1]}   |   {board[0, 2]}   ");
    Console.WriteLine("       |       |       ");
    Console.WriteLine("-------|-------|-------");
    Console.WriteLine("       |       |       ");
    Console.WriteLine($"   {board[1, 0]}   |   {board[1, 1]}   |   {board[1, 2]}   ");
    Console.WriteLine("       |       |       ");
    Console.WriteLine("-------|-------|-------");
    Console.WriteLine("       |       |       ");
    Console.WriteLine($"   {board[2, 0]}   |   {board[2, 1]}   |   {board[2, 2]}   ");
    Console.WriteLine("       |       |       ");
}

static void makeMove(string[,] board, int playerMoveCounter)
{
    int playerTurn = playerMoveCounter % 2 == 0 ? 2 : 1;
    bool valid = false;
    int userInputAsInt = 10;
    while (!valid)
    {
        Console.Write($"Player {playerTurn}: Choose your Field: ");
        string userInput = Console.ReadLine().Trim().ToLower().Substring(0, 1);
        try
        {
            // Checking for valid Number
            userInputAsInt = int.Parse(userInput);

            // Making sure the space is open
            int checkCounter = 0;
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (userInput == board[i, j])
                    {
                        if (playerTurn == 1) board[i, j] = "X";
                        else board[i, j] = "O";
                        valid = true;
                        continue;
                    }
                    checkCounter++;
                }
            }
            if (checkCounter == board.Length) Console.WriteLine("Invalid Moron");
        }
        catch (Exception)
        {
            Console.WriteLine("Nice try asshole, enter a valid field this time.");
            valid = false;
        }
    }


}

static string winChecker(string[,] board)
{
    string totalBoard = "";
    for (int i = 0; i < board.GetLength(0); i++)
    {
        for (int j = 0; j < board.GetLength(1); j++)
        {
            totalBoard += board[i, j]; // Horizontal Checker
        }
        totalBoard += " ";
        for (int j = 0; j < board.GetLength(1); j++)
        {
            totalBoard += board[j, i]; // Vertical Checker
        }
        totalBoard += " ";
    }
    for (int i = 0; i < board.GetLength(0); i++)
    {
        for (int j = 0; j < board.GetLength(1); j++)
        {
            if (i == j) totalBoard += board[i, j]; // Left Diagonal Checker
        }
    }
    totalBoard += " ";
    for (int i = 0; i < board.GetLength(0); i++)
    {
        for (int j = 0; j < board.GetLength(1); j++)
        {
            if (i + j == board.GetLength(0) - 1) totalBoard += board[i, j]; // Right Diagonal Checker
        }
    }

    totalBoard = totalBoard.ToLower();
    bool xWin = totalBoard.IndexOf("xxx") == -1 ? false : true;
    bool oWin = totalBoard.IndexOf("ooo") == -1 ? false : true;
    bool availableMoves = false;

    for (int i = 0; i < totalBoard.Length; i++)
    {
        bool isNumber = int.TryParse(totalBoard.Substring(i, 1), out int number);
        if (isNumber) availableMoves = true;
    }

    string winners;

    if (xWin) winners = "X Wins";
    else if (oWin) winners = "O Wins";
    else if (availableMoves == false) winners = "Cat's Game";
    else winners = "";

    // Console.WriteLine(totalBoard);
    return winners;
}

static void messageAndStats(string[,] board, string winner, int[] globalStats)
{
    switch (winner)
    {
        case "X Wins":
            Console.WriteLine("Congratulations, Player 1!");
            globalStats[0]++;
            break;
        case "O Wins":
            Console.WriteLine("Congratulations, Player 2!");
            globalStats[1]++;
            break;
        case "Cat's Game":
            Console.WriteLine("Draw!");
            globalStats[2]++;
            break;
    }
    globalStats[3]++;
}

static bool playAgain()
{
    Console.WriteLine();
    string userAnswer;
    while (true)
    {
        Console.WriteLine("Would you like to play again?");
        Console.WriteLine("(Y)es or (N)o");
        userAnswer = Console.ReadLine().ToLower().Trim().Substring(0, 1);
        if ("yn".IndexOf(userAnswer) != -1)
        {
            break;
        }
        Console.WriteLine("Re-read my carefully typed instructions fuckhead >:(");
    }
    bool playAgain = false;
    switch (userAnswer)
    {
        case "y":
            playAgain = true;
            break;
        case "n":
            playAgain = false;
            break;
    }
    return playAgain;
}
