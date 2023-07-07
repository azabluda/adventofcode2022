# https://adventofcode.com/2022/day/11
# Day 11: Monkey in the Middle

from collections import Counter
from math import lcm
from functools import reduce

def monkey_in_middle(data):
    for rounds, relief in [20, 3], [10000, 1]:
        # parse input
        items, exprs, mods, choices = [], [], [], []
        for str in data.split('\n\n'):
            arr = str.split('\n')
            items += [*map(int, arr[1].split(': ')[-1].split(', '))],
            exprs += arr[2].split(' = ')[-1],
            mods += int(arr[3].split(' ')[-1]),
            choices += [int(x.split(' ')[-1]) for x in arr[4:]],

        # compute LCM
        mod = reduce(lcm, mods)

        # run simulation
        scores = Counter()
        for _ in range(rounds):
            for i in range(len(mods)):
                while items[i]:
                    old = items[i].pop()
                    new = eval(exprs[i]) // relief
                    nxt = choices[i][new % mods[i] > 0]
                    items[nxt] += new % mod,
                    scores[i] += 1

        # print result
        (_, a), (_, b) = scores.most_common(2)
        print(a * b)


def inputs():
    yield """
Monkey 0:
  Starting items: 79, 98
  Operation: new = old * 19
  Test: divisible by 23
    If true: throw to monkey 2
    If false: throw to monkey 3

Monkey 1:
  Starting items: 54, 65, 75, 74
  Operation: new = old + 6
  Test: divisible by 19
    If true: throw to monkey 2
    If false: throw to monkey 0

Monkey 2:
  Starting items: 79, 60, 97
  Operation: new = old * old
  Test: divisible by 13
    If true: throw to monkey 1
    If false: throw to monkey 3

Monkey 3:
  Starting items: 74
  Operation: new = old + 3
  Test: divisible by 17
    If true: throw to monkey 0
    If false: throw to monkey 1
"""
    yield """
Monkey 0:
  Starting items: 61
  Operation: new = old * 11
  Test: divisible by 5
    If true: throw to monkey 7
    If false: throw to monkey 4

Monkey 1:
  Starting items: 76, 92, 53, 93, 79, 86, 81
  Operation: new = old + 4
  Test: divisible by 2
    If true: throw to monkey 2
    If false: throw to monkey 6

Monkey 2:
  Starting items: 91, 99
  Operation: new = old * 19
  Test: divisible by 13
    If true: throw to monkey 5
    If false: throw to monkey 0

Monkey 3:
  Starting items: 58, 67, 66
  Operation: new = old * old
  Test: divisible by 7
    If true: throw to monkey 6
    If false: throw to monkey 1

Monkey 4:
  Starting items: 94, 54, 62, 73
  Operation: new = old + 1
  Test: divisible by 19
    If true: throw to monkey 3
    If false: throw to monkey 7

Monkey 5:
  Starting items: 59, 95, 51, 58, 58
  Operation: new = old + 3
  Test: divisible by 11
    If true: throw to monkey 0
    If false: throw to monkey 4

Monkey 6:
  Starting items: 87, 69, 92, 56, 91, 93, 88, 73
  Operation: new = old + 8
  Test: divisible by 3
    If true: throw to monkey 5
    If false: throw to monkey 2

Monkey 7:
  Starting items: 71, 57, 86, 67, 96, 95
  Operation: new = old + 7
  Test: divisible by 17
    If true: throw to monkey 3
    If false: throw to monkey 1
"""

for data in inputs():
    monkey_in_middle(data.strip('\n'))
