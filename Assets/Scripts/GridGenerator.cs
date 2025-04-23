using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    const int COL = 13;
    const int ROW = 13;
    const int COL_OFF = 4;
    const int ROW_OFF = 4;

    [SerializeField] GameObject UnbreakableWallPref;
    [SerializeField] GameObject BreakableWallPref;


    void Start()
    {
        GenerateBreakableWalls();
        GenerateUnbrekableWalls();
    }

    void GenerateBreakableWalls()
    {
        bool generateThisTile = false;
        generateThisTile = GenerateSpecialFirstAndLastBreakables(generateThisTile, 0);
        generateThisTile = GenerateSpecialSecondFirstAndLastBreakables(generateThisTile, 1);

        for (int i = 2; i < ROW - 2; i++)
        {
            for (int j = 0; j < COL; j++)
            {
                if (generateThisTile)
                {
                    GameObject newBreakable = Instantiate(BreakableWallPref, gameObject.transform);
                    newBreakable.transform.localPosition = new Vector3(j * COL_OFF, 2, -i * ROW_OFF);
                }
                generateThisTile = !generateThisTile;
            }
        }

        generateThisTile = GenerateSpecialSecondFirstAndLastBreakables(generateThisTile, ROW - 2);
        generateThisTile = GenerateSpecialFirstAndLastBreakables(generateThisTile, ROW - 1);
    }

    private bool GenerateSpecialSecondFirstAndLastBreakables(bool generateThisTile, int row)
    {
        for (int j = 0; j < COL; j++)
        {
            if (generateThisTile)
            {
                if (j != 0 && j != COL - 1)
                {
                    GameObject newBreakable = Instantiate(BreakableWallPref, gameObject.transform);
                    newBreakable.transform.localPosition = new Vector3(j * COL_OFF, 2, -row * ROW_OFF);
                }
            }
            generateThisTile = !generateThisTile;
        }

        return generateThisTile;
    }

    private bool GenerateSpecialFirstAndLastBreakables(bool generateThisTile, int row)
    {
        for (int j = 0; j < COL; j++)
        {
            if (generateThisTile)
            {
                if (j != 1 && j != COL - 2)
                {
                    GameObject newBreakable = Instantiate(BreakableWallPref, gameObject.transform);
                    newBreakable.transform.localPosition = new Vector3(j * COL_OFF, 2, -row * ROW_OFF);
                }
            }
            generateThisTile = !generateThisTile;
        }

        return generateThisTile;
    }

    void GenerateUnbrekableWalls()
    {
        for(int i = 1;  i < ROW; i += 2)
        {
            bool generateThisTile = false;
            
            for (int j = 0; j < COL; j++)
            {
                if (generateThisTile)
                {
                    GameObject newUnbreakable = Instantiate(UnbreakableWallPref, gameObject.transform);
                    newUnbreakable.transform.localPosition = new Vector3(j * COL_OFF, 2, -i * ROW_OFF);
                }
                generateThisTile = !generateThisTile;
            }
        }
    }
}
