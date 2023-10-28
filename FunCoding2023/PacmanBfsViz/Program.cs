using Spectre.Console;
using Spectre.Console.Rendering;

var A = Input().Split("\r\n");
var (m, n) = (A.Length, A[0].Length);
var table = new Table()
    .HideHeaders()
    .HideRowSeparators()
    .NoBorder()
    .AddColumns(Enumerable.Repeat("", n).ToArray());
for (int i = 0; i < m; ++i)
    table.AddEmptyRow();

HashSet<(int, int)> bfs = new(), end = new(), path = new();
Dictionary<(int, int), int> vis = new();
for (int i = 0; i < m; ++i)
    for (int j = 0; j < n; ++j)
        if (A[i][j] == '█')
            vis[(i, j)] = -1;
        else if (A[i][j] == 'S')
            bfs.Add((i, j));
        else if (A[i][j] == 'E')
            end.Add((i, j));

await AnsiConsole
    .Live(table)
    .AutoClear(false)
    .StartAsync(async ctx => 
    {
        var all = Enumerable.Range(0, m).SelectMany(i => Enumerable.Range(0, n).Select(j => (i, j)));
        await RenderAsync(ctx, all);
        for (int dist = 0; bfs.Any(); ++dist)
        {
            foreach (var ij in bfs)
                vis[ij] = dist;
            await RenderAsync(ctx, bfs);
            if (end.Any(bfs.Contains))
                break;
            bfs = bfs.SelectMany(Wasd).Except(vis.Keys).ToHashSet();
        }
        
        for (var ij = end.FirstOrDefault(bfs.Contains); ij != default;)
        {
            path.Add(ij);
            await RenderAsync(ctx, new[] { ij });
            ij = Wasd(ij).FirstOrDefault(IJ => vis.TryGetValue(IJ, out var d) && d == vis[ij] - 1);
        }
    });

Console.WriteLine();

IEnumerable<(int, int)> Wasd((int, int) ij) {
    var (i, j) = ij;
    yield return (i + 1, j);
    yield return (i - 1, j);
    yield return (i, j + 1);
    yield return (i, j - 1);
}

async Task RenderAsync(LiveDisplayContext ctx, IEnumerable<(int, int)> cells) {
    foreach (var (i, j) in cells)
        table.UpdateCell(i, j, CellRenderer(i, j));
    ctx.Refresh();
    await Task.Delay(30);
}

IRenderable CellRenderer(int i, int j) {
    if (!vis.TryGetValue((i, j), out var dist))
        return new Markup("  ");
    if (dist < 0)
        return new Markup("  ", new Style(background: Color.SandyBrown));
    var lbl = end.Contains((i, j)) ? "ex" : dist.ToString().PadLeft(2);
    var rgb = (byte)dist;
    var color = path.Contains((i, j)) ? Color.Blue : new Color(rgb, rgb, rgb);
    return new Markup(lbl, new Style(background: color));
}

string Input() => """
██████████████████████████████████████████████
█S       █  █        █  █  █     █           █
█  ███████  █  ████  █  █  ████  █  █  ████  █
█     █           █                 █  █     █
████  █  █  ██████████  ████  █  █████████████
█        █  █              █  █  █           █
█  ████  █  ███████  █  ███████  █  ███████  █
█  █     █     █     █  █  █  █     █  █     █
█  █  █  █  ████  █  ████  █  █  █  █  ███████
█  █  █  █  █     █     █  █     █        █  █
█████████████  ████  █  █  ███████  █  █  █  █
█     █     █     █  █        █  █  █  █  █  █
█  ███████  ████  ███████  ████  █  █  ████  █
█  █  █     █        █  █        █  █        █
█  █  ████  █  ████  █  ████  ██████████  ████
█     █           █  █     █  █        █     █
█  █  ████  ████  █  █  ████  █  █  ████  █  █
█  █  █        █  █  █           █  █  █  █  █
█  ██████████  ██████████  █  █  █  █  ████  █
█  █                    █  █  █  █     █  █  █
█  ██████████  █  ████  ███████  █  ████  █  █
█        █  █  █     █  █  █     █        █  █
████  █  █  ██████████  █  ███████  ██████████
█     █  █  █        █  █  █           █     █
█  █  ████  █  ███████  █  █  █  ████  ████  █
█  █           █  █  █  █     █  █     █  █  █
███████  █  █  █  █  █  █████████████  █  █  █
█  █     █  █           █        █  █     █  █
█  ███████  ████  ████  █  ███████  █  ████  █
█              █  █        █                 E
██████████████████████████████████████████████
""";