// https://adventofcode.com/2022/day/12
// Day 12: Hill Climbing Algorithm

let hill_climbing (startChars: string, data: string) =

    let folder (area, startPos, endPos) (i, j, ch) =
        let height, endPos' =
            match ch with
            | 'S' -> 0, endPos
            | 'E' -> 25, Some (i, j)
            | _ -> (int ch) - (int 'a'), endPos
        let startPos' =
            match startChars.Contains(ch) with
            | true -> startPos |> Set.add (i, j)
            | _ -> startPos
        let area' = area |> Map.add (i, j) height
        (area', startPos', endPos')

    let area, startPos, endPos =
        data.Trim().Split('\n')
        |> Seq.mapi (fun i line -> line |> Seq.mapi (fun j ch -> i, j, ch))
        |> Seq.collect id
        |> Seq.fold folder (Map.empty, Set.empty, None)

    let bfs_next (queue, seen) =
        let is_valid base_h coord =
            match Map.tryFind coord area with
            | Some h -> h - base_h < 2
            | None -> false
        let valid_moves (i, j) =
            [1; 0; -1; 0; 1]
            |> Seq.pairwise
            |> Seq.map (fun (di, dj) -> i + di, j + dj)
            |> Seq.filter (fun coord -> not (Set.contains coord seen))
            |> Seq.filter (is_valid area.[i, j])
        let queue' =
            queue
            |> Seq.collect valid_moves
            |> Set.ofSeq
        let seen' = seen |> Set.union queue'
        match Set.contains endPos.Value queue' with
        | true -> None
        | _ -> Some (None, (queue', seen'))

    startChars, 1 + Seq.length (Seq.unfold bfs_next (startPos, Set.empty))

let inputdata = [
    """
Sabqponm
abcryxxl
accszExk
acctuvwj
abdefghi
""";

    """
abcccccccccccccccccaaaaaaaaccccccacccaaccccccccccccccccccaaaaaaaaaacccccccccccccccccccccccccccccccaaaaaaccccccccccccccccccccccccccccccccccaaaaa
abccccccccccccccccccaaaaacccccccaaaaaaacccccccccccaaccaaaaaaaaaaaaaccccccccccccccccccccccccccccccccaaaaacccccccccccccccccccccccccccccccccaaaaaa
abccccccccccccaaccccaaaaaacccccccaaaaaaaaccccccacaaaccaaaaaaaaaaaaaaaccccccccccccccccccaaacccccccaaaaaaaccccccccccccccccaaaccccccccccccccaaaaaa
abccccccccacccaaccccaaaaaacccccccaaaaaaaaaccccaaaaaaaaacaaaaaaaaaaaaacccccccccccccccccccaacccccccaaaaaaaacccccccccccccccaaaccccccccccccccaccaaa
abaacccccaaaaaaaccccaaaccacccccccaaaaaaaaaccccaaaaaaaaccccaaaaaaaaaaaccccccccccccccccaacaaaaaccccaaaaaaaacccccccccccccccaaacccccccccccccccccaaa
abaaccccccaaaaaaaacccccccccccccccaaaaaaaaccccccaaaaaacccccaaaacaaaaccccccccccccccccccaaaaaaaaccccccaaacaccccccccccccccccaaakccaaaccccccccccccaa
abaaacccccaaaaaaaaaccccccccccccccaaaaaaacccccccaaaaaccccccaaaccaaaaccccccccccccaacacccaaaaaccccccccaaacccccccccccccacacckkkkkkkaacccccccccccccc
abaaacccccaaaaaaaaaccccccccccccccaccaaaaaccccccaaaaaacccccaaacaaaccccccccccccccaaaaccccaaaaacccccccccccccccccccccccaaaakkkkkkkkkacccaaaccaccccc
abacacccccaaaaaaaccccccccccccccccccccaaaaaaaccccccaaccccccaaaaaaaaccccccccccccaaaaacccaaacaacccccccccccccccccccccccaajkkkkppkkkkccccaaaaaaccccc
abacccccccaaaaaaacccccccccccccccccccaaaaaaaaccccccccccccccccaaaaaaccccccccccccaaaaaacccaacccccccccccccccccccccccccccjjkkooppppkllccccaaaaaccccc
abccccccccaccaaaccccccccccccccccccccaaaaaaaacccccccccccccccccaaaaaccccccccccccacaaaacccccccccccccccccccccccccccccjjjjjjoooppppklllcacaaaaaccccc
abcccaacccccccaaacccccccccccccccccccaaaaaaacccccccccccccccccaaaaacccccccccccccccaacaccccccccccccccccccccccccccjjjjjjjjoooopuppplllcccccaaaacccc
abcccaacccccccccccccccccaaacccccccccccaaaaaaccccccaaaaacccccaaaaaccccccccccccaaacaaacccccaaaccccccccccccccccijjjjjjjjooouuuuuppllllcccccaaacccc
abaaaaaaaaccccccccccccccaaaaccccccccccaacaaaccccccaaaaaccccccccccccccccccccccaaaaaaacccccaaacacccccccccccccciijjoooooooouuuuuppplllllccccaccccc
abaaaaaaaaccccccccccccccaaaaccccccccccaacccccccccaaaaaacccccccccccccccccccccccaaaaaacccaaaaaaaacccccccccccciiiqqooooooouuuxuuuppplllllccccccccc
abccaaaaccccccccccccccccaaaccccccccccccccccccccccaaaaaacccccccccccccccccccccccaaaaaaaccaaaaaaaacccccccccccciiiqqqqtttuuuuxxxuupppqqllllmccccccc
abcaaaaacccaaaccccccccccccccccccccccccaccccccccccaaaaaacccccccccccccccccccccaaaaaaaaaaccaaaaaaccccccccccccciiiqqqtttttuuuxxxuuvpqqqqmmmmccccccc
abcaacaaaccaaacaaccccccccccccccccccccaaaacaaaccccccaacccaaaaacccccccccccccccaaaaaaaaaacccaaaaacccaaaccccccciiiqqttttxxxxxxxyuvvvvqqqqmmmmcccccc
abcacccaaccaaaaaaccccccccccccccccccccaaaaaaaacccccccccccaaaaacccccccccccccccaaacaaacccccaaaaaaccaaaacccccaaiiiqqtttxxxxxxxxyyvvvvvvqqqmmmdddccc
abcccccccaaaaaaaccccccccccccccccccccccaaaaaaaaacccccccccaaaaaaccccccccccccccccccaaaccccccaacccccaaaacccaaaaiiiqqqttxxxxxxxyyyyyyvvvqqqmmmdddccc
SbccccccccaaaaaccccccccaacaaccccccccaaaaaaaaaaccccccccccaaaaaaccccccccccccaaacccaaccccccccccccccaaaacccaaaaaiiiqqtttxxxxEzzyyyyvvvvqqqmmmdddccc
abaccccccccaaaaacccccccaaaaacccccccaaaaaaaaaaaccccccccccaaaaaaccccccccccaaaaaacccccccccccccccccccccccccaaaaaiiiqqqtttxxxyyyyyyvvvvqqqmmmdddcccc
abaacccccccaacaaaccccccaaaaaacccccccaaaaaaaaaaccccccccccccaaacccccccccccaaaaaaccccccccccccccccccccccccccaaaahhhqqqqttxxyyyyyyvvvvqqqmmmddddcccc
abaccccccccaaccccccccccaaaaaacccaacaaccaaaaaaaaaccccccccccccccccccccccccaaaaaaccccccccccccccccccccccccccaaaachhhqqtttxwyyyyyywvrqqqmmmmdddccccc
abaaaccccccccccccccccccaaaaaacccaaaaaccaaaaacaaaccccccccccccccccccccccccaaaaaccccaaaaccccaaaccccccccccccccccchhhppttwwwywwyyywwrrrnmmmdddcccccc
abaaaccccccccccccccccccccaaaccccaaaaaacaaaaaaaaaccccccccaaacccccccccccccaaaaaccccaaaaccccaaaccccccccccccccccchhpppsswwwwwwwwywwrrrnnndddccccccc
abaaacccccccccccccccccccccccccccaaaaaacccaaaaaacccccccccaaaaacccccaacccccccccccccaaaacaaaaaaaaccccccccccccccchhpppsswwwwsswwwwwrrrnneeddccccccc
abaccccccccaaaacccccccccccccccccaaaaaaccccaaaaaaaacccccaaaaaaccaacaaacccccccccccccaaccaaaaaaaaccccccccccccccchhpppssssssssrwwwwrrrnneeecaaccccc
abaccccccccaaaacccccccccccccccccccaaaccccaaaaaaaaacccccaaaaaaccaaaaaccccccccccccccccccccaaaaacccccccccccccccchhpppssssssssrrrwrrrnnneeeaaaccccc
abcccccccccaaaacccccccccccccccccccccccccaaaaaaaaaaccccccaaaaacccaaaaaacccccccccccccccccaaaaaacccccccccccccccchhpppppsssooorrrrrrrnnneeeaaaccccc
abcccccccccaaaccccccccccccccccccccccccccaaacaaacccccccccaacaacaaaaaaaacccccccccccccccccaaaaaacaaccccccccccccchhhppppppoooooorrrrnnneeeaaaaacccc
abccccccccccccccccccccccccccccccccccccccccccaaaccaaaacccccccccaaaaacaaccccaacccccccccacaaaaaacaaccccccccccccchhhgpppppoooooooonnnnneeeaaaaacccc
abcccccccaacccccccccccccccccccccccccccccccccaaacaaaaaccccccccccacaaaccccccaacccccccccaacaaaaaaaaaaacccccaaccccgggggggggggfooooonnneeeeaaaaacccc
abcccccccaaacaaccccccccccccaacccccccccccccccccccaaaaaaccccaacccccaaacccaaaaaaaaccccccaaaaacaaaaaaaaccccaaacccccggggggggggfffooonneeeecaaacccccc
abcccccccaaaaaaccccaacccccaaacccccccccccccccccccaaaaaaccccaaaccccccccccaaaaaaaacccccccaaaaaccaaaaccccaaaaaaaacccggggggggfffffffffeeeecaaccccccc
abcccccaaaaaaaccaaaaacaaaaaaacccccccccccccccccccaaaaacccccaaaacccaaccccccaaaacccccccaaaaaaaacaaaaacccaaaaaaaaccccccccccaaaffffffffecccccccccccc
abcaaacaaaaaaacccaaaaaaaaaaaaaaaccccccccccccccccccaaacccccaaaacaaaacaacccaaaaaccccccaaaaaaaaaaaaaaccccaaaaaacccccccccccaaacaafffffccccccccccaaa
abaaaacccaaaaaaccaaaaacaaaaaaaaaccccccccccccaaacccccccccccaaaaaaaaacaaccaaacaacccccccccaacccaaccaaccccaaaaaaccccccccccaaaaccaaacccccccccccccaaa
abaaaacccaacaaacaaaaacccaaaaaaacccccccccccccaaaacccccccccaaaaaaaaaaaaaccaacccacccccccccaacccccccccccccaaaaaaccccccccccaaacccccccccccccccccccaaa
abcaaacccaacccccccaaaccaaaaaacccccccccccccccaaaaccccccaaaaaaaaaaaaaaaaaaccccccccccccccccccccccccccccccaaccaaccccccccccaaaccccccccccccccccaaaaaa
abcccccccccccccccccccccaaaaaaaccccccccccccccaaacccccccaaaaaaaaaaaaaaaaaacccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccaaaaaa
"""]

let product X Y = 
    X |> Seq.collect (fun x -> Y |> Seq.map (fun y -> x, y))

product ["S"; "Sa"] inputdata |> Seq.map hill_climbing |> printfn "%A"
