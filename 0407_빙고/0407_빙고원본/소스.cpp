#include <iostream>
#include <time.h>

using namespace std;

void shuffle(int arr[][5])
{
    int destCol, destRow, sourCol, sourRow, temp;
    for (int i = 0; i < 77; i++)
    {
        destCol = rand() % 5;
        destRow = rand() % 5;
        sourCol = rand() % 5;
        sourRow = rand() % 5;

        temp = arr[destRow][destCol];
        arr[destRow][destCol] = arr[sourRow][sourCol];
        arr[sourRow][sourCol] = temp;
    }
}

void enterNum(int arr[][5], const int& num)
{
    for (int i = 0; i < 5; i++)
    {
        for (int j = 0; j < 5; j++)
        {
            if (arr[i][j] == num)
                arr[i][j] = -1;
        }
    }
}

void checkNum(int arr[][5], int* ldig, int* rdig, int* bingo)
{
    for (int i = 0; i < 5; i++)
    {
        int rowBingo = 0;
        int colBingo = 0;

        for (int j = 0; j < 5; j++)
        {
            if (arr[i][j] == -1)
            {
                if (i == 2 && j == 2)
                {
                    (*ldig)++;
                    (*rdig)++;
                }
                else if (i + j == 4)
                    (*rdig)++;
                else if (i == j)
                    (*ldig)++;
                rowBingo++;
            }
            if (arr[j][i] == -1)
            {
                colBingo++;
            }

            if (colBingo == 5) (*bingo)++;
            if (rowBingo == 5) (*bingo)++;
        }
    }
}

void printBingo(int arr[][5], int arr2[][5])
{
    for (int i = 0; i < 5; i++)
    {
        for (int j = 0; j < 5; j++)
        {
            if (arr[i][j] == -1)
                cout << "#";
            else
                cout << arr[i][j];
            cout << "\t";
        }
        cout << "\t\t";
        for (int j = 0; j < 5; j++)
        {
            if (arr2[i][j] == -1)
                cout << "#";
            else
                cout << arr2[i][j];

            cout << "\t";
        }
        cout << endl << endl;
    }
    cout << endl;
}

int main()
{
    int numPlayer[5][5], numCom[5][5];
    int inputNum = 0, bingoPlayer = 0, bingoCom = 0;
    bool playerWin = false;

    srand(time(NULL));

    // �⺻ ���ڷ� �ʱ�ȭ�Ѵ�. (0 ~ 25)
    for (int i = 0; i < 5; i++)
    {
        for (int j = 0; j < 5; j++)
        {
            numPlayer[i][j] = 5 * i + j + 1;
            numCom[i][j] = 5 * i + j + 1;
        }
    }

    // ���ڸ� �����ϰ� ���´�.
    shuffle(numPlayer);
    shuffle(numCom);

    cout << "\n\t\t\t\t\t~ ���� ���� ~\n" << endl;
    while (true)
    {
        int colBingo = 0, leftDigBingo = 0, rightDigBingo = 0;
        int colBingoCom = 0, leftDigBingoCom = 0, rightDigBingoCom = 0;

        printBingo(numPlayer, numCom);

        cout << "���� �Է�: ";
        cin >> inputNum;
        // �߸��� ���� ������ ���, continue�� ���� �� ó������ ���ư���.
        if (inputNum < 1 || inputNum > 25) {
            cout << "�߸��� �Է��Դϴ�. �ٽ� �Է��� �ּ���." << endl << endl;
            continue;
        }

        // ����ڷκ��� �Է¹��� ���ڸ� ó���Ѵ�.
        enterNum(numPlayer, inputNum);
        enterNum(numCom, inputNum);


        // ��ǻ���� �Է��� ó���� ������ �����Ѵ�.
        int inputNumCom;

        // check: 0 ~ 4�� ����, 5 ~ 9�� ����, 10�� ���� ����� �밢��\, 11�� ���� ����� �밢��/
        // �� ����/����/�밢���� -1�� �󸶳� ����Ǿ� �ִ����� �����Ѵ�.
        // numbers: ���� �� ���� �����ϰ�, ���� ���� -1�� ���� ���� ���ڸ� �����Ѵ�.
        int check[12]{ 0 };
        int numbers[5]{ 0 };

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (numCom[i][j] == -1)
                {
                    if (i == 2 && j == 2)
                    {
                        check[10]++;
                        check[11]++;
                    }
                    else if (i == j) check[10]++;
                    else if (i + j == 4) check[11]++;
                    check[i]++;
                }
                if (numCom[j][i] == -1)
                {
                    check[5 + i]++;
                }
            }
        }

        // ���� ū ���� ã�´�. ���� ���� �ִٸ� �켱 ������ ���� > ���� > �밢���̴�.
        // 5�� �̹� ���� �� ���̱� ������ �����Ѵ�.
        int a = 0, idx = 0;
        for (int i = 0; i < 12; i++)
        {
            if (check[i] != 5 && a < check[i])
            {
                a = check[i];
                idx = i;
            }
        }

        //idx�� ���� ó���ؾ� �ϴµ�
        if (idx / 5 == 0) // ���� 0�� ���, 0~4
        {
            for (int i = 0; i < 5; i++)
                numbers[i] = numCom[idx][i];
        }
        else if (idx / 5 == 1) // 5~9
        {
            for (int i = 0; i < 5; i++)
                numbers[i] = numCom[i][idx % 5];
        }
        else // 10,11
        {
            if (idx == 10)
            {
                for (int i = 0; i < 5; i++)
                    numbers[i] = numCom[i][i];
            }
            else
            {
                for (int i = 0; i < 5; i++)
                    numbers[i] = numCom[i][4 - i];
            }
        }

        while (true)
        {
            int t_idx = rand() % 5;
            if (numbers[t_idx] != -1)
            {
                inputNumCom = numbers[t_idx];
                break;
            }
        }

        enterNum(numPlayer, inputNumCom);
        enterNum(numCom, inputNumCom);

        checkNum(numPlayer, &leftDigBingo, &rightDigBingo, &bingoPlayer);
        checkNum(numCom, &leftDigBingoCom, &rightDigBingoCom, &bingoCom);

        if (rightDigBingo == 5)
            bingoPlayer++;
        if (leftDigBingo == 5)
            bingoPlayer++;
        if (rightDigBingoCom == 5)
            bingoCom++;
        if (leftDigBingoCom == 5)
            bingoCom++;

        cout << endl;
        cout << "\t>> ����� ����: " << inputNum << " << \t\t\t\t >> ��ǻ���� ����: " << inputNumCom << " <<" << endl;
        cout << "\t>> ���� ī��Ʈ: " << bingoPlayer << " << \t\t\t\t >> ���� ī��Ʈ: " << bingoCom << " <<" << endl;
        cout << endl;

        if (bingoPlayer == 5 || bingoCom == 5)
        {
            break;
        }
        bingoPlayer = 0;
        bingoCom = 0;
    }

    printBingo(numPlayer, numCom);

    if (bingoPlayer >= bingoCom)
    {
        cout << "�¸��ϼ̽��ϴ�!" << endl;
    }
    else
    {
        cout << "��ǻ���� �¸��Դϴ�." << endl;
    }

    return 0;
}