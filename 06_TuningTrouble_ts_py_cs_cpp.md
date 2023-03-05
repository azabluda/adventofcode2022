--- Day 6: Tuning Trouble ---

https://adventofcode.com/2022/day/6

#### TypeScript
```ts
for (const s of inputsDay06())
    for (const n of [4, 14])
        for (let i = n;; ++i)
            if (new Set<string>(s.slice(i-n, i)).size === n) {
                console.log(`last ${n} chars of '${s.slice(0, 5)}...' are unique at ${i}`)
                break
            }
```
#### Python
```py
for s in inputs():
    for n in 4, 14:
        print(next(i for i in count(n) if len(set(s[i-n:i]))==n))
```
#### C#
```cs
Console.WriteLine(string.Concat(
    from s in Inputs()
    from n in new[] { 4, 14 }
    let i = Enumerable.Range(n, s.Length).First(i => s.Substring(i - n, n).Distinct().Count() == n)
    select $"last {n} chars of '{s.Substring(0, 5)}...' are unique at {i}\n"));
```
#### C++
```cpp
for (auto s : inputs())
    for (auto n : { 4, 14 })
        for (auto i = begin(s);; ++i)
            if (size(set<char>(i, i + n)) == n) {
                cout << i - begin(s) + n << endl;
                break;
            }
```
