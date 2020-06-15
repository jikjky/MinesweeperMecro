using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

public delegate void Update();

namespace MinesweeperMecro
{

    public class BoxInformation
    {
        public enum State
        {
            NoneClick,
            Empty,
            Mine,
            NeedClick,
            Number,
        }
        public Rectangle myRectangle;

        public int percent;

        public Point ClickPoint;

        public bool isClick = false;
        public int number = 0;
        public State myState = State.NoneClick;
    }

    public class ColRow
    {
        public int col;
        public int row;
        public ColRow(int col, int row)
        {
            this.col = col;
            this.row = row;
        }

    }
    public class NoneClick
    {
        public int NoneClickCount = 0;
        public List<ColRow> Info = new List<ColRow>();
    }

    public class Mine
    {
        public int MineCount = 0;
        public List<ColRow> Info = new List<ColRow>();
    }

    public class Empty
    {
        public int EmptyCount = 0;
        public List<ColRow> Info = new List<ColRow>();
    }

    public class MinesweeperMecro
    {
        public event Update UpdateEvent;

        public int startX = 632;
        public int startY = 336;
        public int endX = 1472;
        public int endY = 784;

        public Rectangle MinesweeperSpace;

        public int col = 30;
        public int row = 16;

        public int boxSize = 28;

        public BoxInformation[,] colInformation;

        public int TotalMineCount = 99;

        Bitmap currentImage;

        Bitmap Empty;
        Bitmap NoneClick;
        Bitmap Mine1;
        Bitmap Mine2;
        Bitmap Mine3;
        Bitmap Mine4;
        Bitmap Mine5;
        Bitmap Mine6;

        Bitmap Win;

        Bitmap Fail;

        public MinesweeperMecro()
        {
            Empty = new Bitmap("Resources//Empty.png");
            NoneClick = new Bitmap("Resources//NoneClick.png");
            Mine1 = new Bitmap("Resources//1.png");
            Mine2 = new Bitmap("Resources//2.png");
            Mine3 = new Bitmap("Resources//3.png");
            Mine4 = new Bitmap("Resources//4.png");
            Mine5 = new Bitmap("Resources//5.png");
            Mine6 = new Bitmap("Resources//6.png");


            Win = new Bitmap("Resources//Win.png");

            Fail = new Bitmap("Resources//Fail.png");

            Init();
        }

        public void Init()
        {
            MinesweeperSpace = new Rectangle(startX, startY, endX - startX, endY - startY);
            colInformation = new BoxInformation[col, row];
            for (int i = 0; i < col; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    colInformation[i, j] = new BoxInformation();
                    colInformation[i, j].myRectangle = new Rectangle(i * boxSize, j * boxSize, boxSize, boxSize);
                    colInformation[i, j].ClickPoint = new Point(MinesweeperSpace.X + colInformation[i, j].myRectangle.X + boxSize / 2, MinesweeperSpace.Y + colInformation[i, j].myRectangle.Y + boxSize / 2);
                }
            }
        }

        public void Start()
        {
            Init();

            Util.MouseClick(1029 + 20, 266 + 20);
            Thread.Sleep(300);
            Util.MouseClick(colInformation[col / 2, row / 2].ClickPoint.X, colInformation[col / 2, row / 2].ClickPoint.Y);
            Thread.Sleep(100);

            while (true)
            {
                Bitmap SmileImage = Util.ClopImage(Util.PrintScreenToImage(), new Rectangle(1029,266,1073-1029,310-266));
                /*if (Util.ImageCompare(SmileImage, Win))
                {
                    Init();
                    SmileImage.Dispose();
                    break;
                }   
                else */if (Util.ImageCompare(SmileImage, Win) || Util.ImageCompare(SmileImage, Fail))
                {
                    Init();

                    Util.MouseClick(1029 + 20, 266 + 20);
                    Thread.Sleep(300);
                    Util.MouseClick(colInformation[col / 2, row / 2].ClickPoint.X, colInformation[col / 2, row / 2].ClickPoint.Y);
                    Thread.Sleep(100);
                }

                SmileImage.Dispose();
                Update();

                GC.Collect();
            }
        }

        public void Update()
        {
            Thread.Sleep(100);
            UpdateEvent();
            Application.DoEvents();
            currentImage?.Dispose();
            currentImage = Util.ClopImage(Util.PrintScreenToImage(), MinesweeperSpace);

            //이미지 to Memory
            for (int i = 0; i < col; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    //퍼센트 초기화
                    colInformation[i, j].percent = -1;
                    //
                    if (colInformation[i, j].myState == BoxInformation.State.NoneClick || colInformation[i, j].myState == BoxInformation.State.NeedClick)
                    {
                        Bitmap tempBitmap = Util.ClopImage(currentImage, colInformation[i, j].myRectangle);

                        if (Util.ImageCompare(tempBitmap, Empty) == true)
                        {
                            colInformation[i, j].myState = BoxInformation.State.Empty;
                        }
                        else if (Util.ImageCompare(tempBitmap, NoneClick) == true)
                        {
                            colInformation[i, j].myState = BoxInformation.State.NoneClick;

                        }
                        else if (Util.ImageCompare(tempBitmap, Mine1) == true)
                        {
                            colInformation[i, j].myState = BoxInformation.State.Number;
                            colInformation[i, j].number = 1;
                        }
                        else if (Util.ImageCompare(tempBitmap, Mine2) == true)
                        {
                            colInformation[i, j].myState = BoxInformation.State.Number;
                            colInformation[i, j].number = 2;
                        }
                        else if (Util.ImageCompare(tempBitmap, Mine3) == true)
                        {
                            colInformation[i, j].myState = BoxInformation.State.Number;
                            colInformation[i, j].number = 3;
                        }
                        else if (Util.ImageCompare(tempBitmap, Mine4) == true)
                        {
                            colInformation[i, j].myState = BoxInformation.State.Number;
                            colInformation[i, j].number = 4;
                        }
                        else if (Util.ImageCompare(tempBitmap, Mine5) == true)
                        {
                            colInformation[i, j].myState = BoxInformation.State.Number;
                            colInformation[i, j].number = 5;
                        }
                        else if (Util.ImageCompare(tempBitmap, Mine6) == true)
                        {
                            colInformation[i, j].myState = BoxInformation.State.Number;
                            colInformation[i, j].number = 6;
                        }

                        tempBitmap.Dispose();
                    }
                }
            }
        //Process
        Start:
            for (int i = 0; i < col; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    if (colInformation[i, j].myState == BoxInformation.State.Number)
                    {
                        NoneClick tempNoneClick = new NoneClick();
                        Mine tempMine = new Mine();
                        Empty tempEmpty = new Empty();

                        //■□□
                        //□□□
                        //□□□
                        if (i != 0 && j != 0)
                        {
                            if (colInformation[i - 1, j - 1].myState == BoxInformation.State.NoneClick)
                            {
                                tempNoneClick.NoneClickCount++;
                                tempNoneClick.Info.Add(new ColRow(i - 1, j - 1));
                            }
                            else if (colInformation[i - 1, j - 1].myState == BoxInformation.State.Mine)
                            {
                                tempMine.MineCount++;
                                tempMine.Info.Add(new ColRow(i - 1, j - 1));
                            }
                            else if (colInformation[i - 1, j - 1].myState == BoxInformation.State.Empty)
                            {
                                tempEmpty.EmptyCount++;
                                tempEmpty.Info.Add(new ColRow(i - 1, j - 1));
                            }
                        }
                        //□■□
                        //□□□
                        //□□□
                        if (j != 0)
                        {
                            if (colInformation[i, j - 1].myState == BoxInformation.State.NoneClick)
                            {
                                tempNoneClick.NoneClickCount++;
                                tempNoneClick.Info.Add(new ColRow(i, j - 1));
                            }
                            else if (colInformation[i, j - 1].myState == BoxInformation.State.Mine)
                            {
                                tempMine.MineCount++;
                                tempMine.Info.Add(new ColRow(i, j - 1));
                            }
                            else if (colInformation[i, j - 1].myState == BoxInformation.State.Empty)
                            {
                                tempEmpty.EmptyCount++;
                                tempEmpty.Info.Add(new ColRow(i, j - 1));
                            }
                        }
                        //□□■
                        //□□□
                        //□□□
                        if (j != 0 && i != col - 1)
                        {
                            if (colInformation[i + 1, j - 1].myState == BoxInformation.State.NoneClick)
                            {
                                tempNoneClick.NoneClickCount++;
                                tempNoneClick.Info.Add(new ColRow(i + 1, j - 1));
                            }
                            else if (colInformation[i + 1, j - 1].myState == BoxInformation.State.Mine)
                            {
                                tempMine.MineCount++;
                                tempMine.Info.Add(new ColRow(i + 1, j - 1));
                            }
                            else if (colInformation[i + 1, j - 1].myState == BoxInformation.State.Empty)
                            {
                                tempEmpty.EmptyCount++;
                                tempEmpty.Info.Add(new ColRow(i + 1, j - 1));
                            }
                        }
                        //□□□
                        //■□□
                        //□□□
                        if (i != 0)
                        {
                            if (colInformation[i - 1, j].myState == BoxInformation.State.NoneClick)
                            {
                                tempNoneClick.NoneClickCount++;
                                tempNoneClick.Info.Add(new ColRow(i - 1, j));
                            }
                            else if (colInformation[i - 1, j].myState == BoxInformation.State.Mine)
                            {
                                tempMine.MineCount++;
                                tempMine.Info.Add(new ColRow(i - 1, j));
                            }
                            else if (colInformation[i - 1, j].myState == BoxInformation.State.Empty)
                            {
                                tempEmpty.EmptyCount++;
                                tempEmpty.Info.Add(new ColRow(i - 1, j));
                            }
                        }
                        //□□□
                        //□□■
                        //□□□
                        if (i != col - 1)
                        {
                            if (colInformation[i + 1, j].myState == BoxInformation.State.NoneClick)
                            {
                                tempNoneClick.NoneClickCount++;
                                tempNoneClick.Info.Add(new ColRow(i + 1, j));
                            }
                            else if (colInformation[i + 1, j].myState == BoxInformation.State.Mine)
                            {
                                tempMine.MineCount++;
                                tempMine.Info.Add(new ColRow(i + 1, j));
                            }
                            else if (colInformation[i + 1, j].myState == BoxInformation.State.Empty)
                            {
                                tempEmpty.EmptyCount++;
                                tempEmpty.Info.Add(new ColRow(i + 1, j));
                            }
                        }
                        //□□□
                        //□□□
                        //■□□
                        if (i != 0 && j != row - 1)
                        {
                            if (colInformation[i - 1, j + 1].myState == BoxInformation.State.NoneClick)
                            {
                                tempNoneClick.NoneClickCount++;
                                tempNoneClick.Info.Add(new ColRow(i - 1, j + 1));
                            }
                            else if (colInformation[i - 1, j + 1].myState == BoxInformation.State.Mine)
                            {
                                tempMine.MineCount++;
                                tempMine.Info.Add(new ColRow(i - 1, j + 1));
                            }
                            else if (colInformation[i - 1, j + 1].myState == BoxInformation.State.Empty)
                            {
                                tempEmpty.EmptyCount++;
                                tempEmpty.Info.Add(new ColRow(i - 1, j + 1));
                            }
                        }
                        //□□□
                        //□□□
                        //□■□
                        if (j != row - 1)
                        {
                            if (colInformation[i, j + 1].myState == BoxInformation.State.NoneClick)
                            {
                                tempNoneClick.NoneClickCount++;
                                tempNoneClick.Info.Add(new ColRow(i, j + 1));
                            }
                            else if (colInformation[i, j + 1].myState == BoxInformation.State.Mine)
                            {
                                tempMine.MineCount++;
                                tempMine.Info.Add(new ColRow(i, j + 1));
                            }
                            else if (colInformation[i, j + 1].myState == BoxInformation.State.Empty)
                            {
                                tempEmpty.EmptyCount++;
                                tempEmpty.Info.Add(new ColRow(i, j + 1));
                            }
                        }
                        //□□□
                        //□□□
                        //□□■
                        if (i != col - 1 && j != row - 1)
                        {
                            if (colInformation[i + 1, j + 1].myState == BoxInformation.State.NoneClick)
                            {
                                tempNoneClick.NoneClickCount++;
                                tempNoneClick.Info.Add(new ColRow(i + 1, j + 1));
                            }
                            else if (colInformation[i + 1, j + 1].myState == BoxInformation.State.Mine)
                            {
                                tempMine.MineCount++;
                                tempMine.Info.Add(new ColRow(i + 1, j + 1));
                            }
                            else if (colInformation[i + 1, j + 1].myState == BoxInformation.State.Empty)
                            {
                                tempEmpty.EmptyCount++;
                                tempEmpty.Info.Add(new ColRow(i + 1, j + 1));
                            }
                        }

                        //Log
                        string logText = "";
                        for (int ic = 0; ic < row; ic++)
                        {
                            for (int jc = 0; jc < col; jc++)
                            {
                                if (colInformation[jc, ic].myState == BoxInformation.State.Empty)
                                {
                                    logText += "□";
                                }
                                else if (colInformation[jc, ic].myState == BoxInformation.State.Mine)
                                {
                                    logText += "☆";
                                }
                                else if (colInformation[jc, ic].myState == BoxInformation.State.NoneClick)
                                {
                                    logText += "■";
                                }
                                else if (colInformation[jc, ic].myState == BoxInformation.State.Number && colInformation[jc, ic].number == 1)
                                {
                                    logText += "①";
                                }
                                else if (colInformation[jc, ic].myState == BoxInformation.State.Number && colInformation[jc, ic].number == 2)
                                {
                                    logText += "②";
                                }
                                else if (colInformation[jc, ic].myState == BoxInformation.State.Number && colInformation[jc, ic].number == 3)
                                {
                                    logText += "③";
                                }
                                else if (colInformation[jc, ic].myState == BoxInformation.State.Number && colInformation[jc, ic].number == 4)
                                {
                                    logText += "④";
                                }
                                else if (colInformation[jc, ic].myState == BoxInformation.State.Number && colInformation[jc, ic].number == 5)
                                {
                                    logText += "⑤";
                                }
                                else if (colInformation[jc, ic].myState == BoxInformation.State.Number && colInformation[jc, ic].number == 6)
                                {
                                    logText += "⑥";
                                }
                                else if (colInformation[jc, ic].myState == BoxInformation.State.NeedClick)
                                {
                                    logText += "○";
                                }
                            }
                            logText += '\n';
                        }

                        Debug.WriteLine(logText);

                        //지뢰 감지
                        if (tempNoneClick.NoneClickCount + tempMine.MineCount == colInformation[i, j].number)
                        {
                            foreach (var item in tempNoneClick.Info)
                            {
                                colInformation[item.col, item.row].myState = BoxInformation.State.Mine;
                                colInformation[item.col, item.row].percent = 100;
                                goto Start;
                            }
                        }

                        //지뢰 있을 확률계산
                        if ((tempNoneClick.NoneClickCount > 0 || tempMine.MineCount > 0) && tempNoneClick.NoneClickCount != tempMine.MineCount)
                        {
                            int ifMine = tempNoneClick.NoneClickCount + tempMine.MineCount;

                             int value = Convert.ToInt32( (float)colInformation[i, j].number / (float)ifMine * 100);

                            foreach (var item in tempNoneClick.Info)
                            {
                                if (colInformation[item.col, item.row].percent < value)
                                {
                                    colInformation[item.col, item.row].percent = value;
                                    Debug.WriteLine("col : " + item.col + "row : " + item.row + " == " + colInformation[item.col, item.row].percent);
                                }
                            }
                        }

                        //지뢰 없는곳
                        if (tempMine.MineCount == colInformation[i, j].number)
                        {
                            foreach (var item in tempNoneClick.Info)
                            {
                                colInformation[item.col, item.row].myState = BoxInformation.State.NeedClick;
                                colInformation[item.col, item.row].percent = 0;
                            }
                        }
                    }
                }
            }

            int ClickCount = 0;
            int NoneClickCount = 0;
            int MineCount = 0;
            for (int i = 0; i < col; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    if (colInformation[i, j].myState == BoxInformation.State.NeedClick)
                    {
                        ClickCount++;
                        Util.MouseClick(colInformation[i, j].ClickPoint.X, colInformation[i, j].ClickPoint.Y);
                        Thread.Sleep(100);
                    }
                    if (colInformation[i, j].myState == BoxInformation.State.Mine)
                    {
                        MineCount++;
                    }
                    if (colInformation[i, j].myState == BoxInformation.State.NoneClick)
                    {
                        NoneClickCount++;
                    }
                }
            }

            if (ClickCount == 0)
            {
                for (int i = 0; i < col; i++)
                {
                    for (int j = 0; j < row; j++)
                    {
                        if (colInformation[i, j].myState == BoxInformation.State.NoneClick)
                        {
                            if (colInformation[i, j].percent == -1)
                            {
                                colInformation[i, j].percent = Convert.ToInt32(((float)TotalMineCount - (float)MineCount) / NoneClickCount * 100);
                            }
                        }
                    }
                }

                int tempCol = 0;
                int tempRow = 0;
                int minPercent = 100;
                for (int i = 0; i < col; i++)
                {
                    for (int j = 0; j < row; j++)
                    {
                        if (colInformation[i, j].percent < minPercent && colInformation[i, j].myState == BoxInformation.State.NoneClick)
                        {
                            tempCol = i;
                            tempRow = j;
                            minPercent = colInformation[i, j].percent;

                            Debug.WriteLine("Min : " + colInformation[i, j].percent);
                        }
                    }
                }

                Util.MouseClick(colInformation[tempCol, tempRow].ClickPoint.X, colInformation[tempCol, tempRow].ClickPoint.Y);
                Thread.Sleep(100);
                return;
            }


        }
    }
}
