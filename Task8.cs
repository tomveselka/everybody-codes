namespace EverybodyCodes2024;

public class Task8
{
    public void Part1()
    {
        var availableBlocks = 4097737;
        int levelWidth = 1;
        int usedUpBlocks = 1;
        bool continueLoop = true;
        int finalWidth = 0;
        int missingBlocks = 0;
        while (continueLoop)
        {
            if(usedUpBlocks+levelWidth+2>=availableBlocks) 
            {
                finalWidth = levelWidth + 2;
                missingBlocks = Math.Abs(availableBlocks - (usedUpBlocks + finalWidth));
                continueLoop = false;
            }
            else
            {
                levelWidth += 2;
                usedUpBlocks += levelWidth;
            }
        }
        Console.WriteLine("Final width is {0}, missing blocks number is {1}. Result is {2}", finalWidth, missingBlocks, finalWidth * missingBlocks);
    }

    public void Part2()
    {
        var availableBlocks = 20240000;
        var priests = 947;
        var acolytes = 1111;

        var thickness = 1;
        var width = 1;
        var usedUpBlocks = 1;
        bool continueLoop = true;
        var missingBlocks = 0;
        while (continueLoop)
        {
            thickness = (thickness * priests) % acolytes;
            width += 2;

            var blocksPerLayer = thickness * width;
            usedUpBlocks += blocksPerLayer;
            if (usedUpBlocks > availableBlocks)
            {
                missingBlocks = usedUpBlocks - availableBlocks;
                continueLoop = false;
            }
        }
        Console.WriteLine("Final width is {0}, missing blocks number is {1}. Result is {2}", width, missingBlocks, width * missingBlocks);

    }

    public void Part3()
    {
        var availableBlocks = 160;
        var priests = 2;
        var acolytes = 5;

        var thickness = 1;
        var width = 1;
        var usedUpBlocks = 1;
        bool continueLoop = true;
        var missingBlocks = 0;
        while (continueLoop)
        {
            thickness = ((thickness * priests) % acolytes) + acolytes;
            width += 2;

            var blocksPerLayer = thickness * width;
            usedUpBlocks += blocksPerLayer;
            if (usedUpBlocks > availableBlocks)
            {
                missingBlocks = usedUpBlocks - availableBlocks;
                continueLoop = false;
            }
        }
        Console.WriteLine("Final width is {0}, missing blocks number is {1}. Result is {2}", width, missingBlocks, width * missingBlocks);

    }
}
