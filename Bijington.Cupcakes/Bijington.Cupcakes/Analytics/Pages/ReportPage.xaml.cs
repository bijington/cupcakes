namespace Bijington.Cupcakes.Analytics.Pages;

public partial class ReportPage : ContentPage, IDrawable
{
    public ReportPage()
    {
        InitializeComponent();
        Chart.Drawable = this;
    }

    private readonly IList<Bar> _bars = [];
    private float _chartHeight;

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);

        if (width <= 0 || height <= 0)
        {
            return;
        }
        
        _bars.Clear();
        
        const float spacing = 20f;
        const int columns = 7;
        var chartWidth = (float)width * 0.5f;
        
        var columnWidth = (chartWidth - (columns + 1 * spacing)) / columns;

        IList<float> values = [0.8f, 0.5f, 0.4f, 0.2f, 0.1f, 0.9f, 0.75f];
        IList<Color> colors =
            [Colors.Red, Colors.Orange, Colors.Yellow, Colors.Green, Colors.Blue, Colors.Indigo, Colors.Violet];
        
        _chartHeight = (float)height * 0.8f;

        for (var i = 0; i < columns; i++)
        {
            _bars.Add(new Bar
            {
                Width = columnWidth,
                Height = values[i] * _chartHeight,
                Color = colors[i],
                Index = i
            });
        }
        
        new Animation(
                v =>
                {
                    foreach (var bar in _bars)
                    {
                        bar.Progress = (float)v;
                    }

                    this.Chart.Invalidate();
                },
                0,
                1,
                Easing.SpringOut)
            .Commit(
                this,
                "Load",
                length: 750);
    }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        foreach (var bar in _bars)
        {
            canvas.FillColor = bar.Color;

            var barHeight = bar.Height * bar.Progress;
            var columnWidth = bar.Width;
            const float spacing = 20f; 

            canvas.FillRectangle(
                spacing + (columnWidth + spacing) * bar.Index,
                _chartHeight - barHeight,
                columnWidth,
                barHeight);
        }
    }
}

public class Bar
{
    public int Index { get; init; }
    public float Progress { get; set; }
    public float Height { get; init; }
    
    public float Width { get; init; }
    
    public Color Color { get; init; }
}