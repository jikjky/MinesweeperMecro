# MinesweeperMecro

시연영상<br>
https://youtu.be/-qKUdNlEpJI

## 알고리즘

![Imgur](https://i.imgur.com/pPY3od9.png)

남은 지뢰수 : 10

임의의 곳 클릭

![Imgur](https://i.imgur.com/dH3zv4Y.png)

남은 지뢰수 : 10

![Imgur](https://i.imgur.com/6Egzh6V.png)

숫자를 기반으로 확률 계산

확률이 겹쳐저 있으면 높은것을 기억

![Imgur](https://i.imgur.com/zY8lgqt.png)

남은 지뢰수 : 2

100% 인곳은 지뢰이므로 지뢰를 제외하고 확률 계산

![Imgur](https://i.imgur.com/TxVWqaI.png)

남은 지뢰수 : 2

확률이 0% 인곳은 지뢰가 없는 곳이므로 클릭

![Imgur](https://i.imgur.com/mBK0iqZ.png)

남은 지뢰수 : 2

![Imgur](https://i.imgur.com/LudzhyI.png)

남은 지뢰수 : 1

![Imgur](https://i.imgur.com/eNrdhZD.png)

남은 지뢰수 : 1

0%나 100%가 없을 경우 지뢰일 확률이 가장 낮은곳중 임의 클릭

숫자가 적혀 있지 않은 확률을 계산할수 없는 곳은 남은 지뢰수 / 누를수 있는 곳 으로 확률 계산

위에 경우 1 / 4 = 0.25

25%의 확률을 부여

위의 경우 50% 3가지중 하나 클릭

![Imgur](https://i.imgur.com/5Way4d1.png)

남은 지뢰수 : 1

![Imgur](https://i.imgur.com/2lTGR0e.png)

남은 지뢰수 : 0
