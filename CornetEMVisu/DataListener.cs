using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CornetEMVisu
{
    //public abstract class DataPlotterBase
    //{
    //    public int pointAmount = 20;
    //    protected int refreshEvery = 0;

    //    public DataPlotterBase(PlotModel model)
    //    {
    //        this.model = model;
    //    }

    //    public abstract void AddPoint(double point);
    //}

    //public class DataPlotterSimpleBar : DataPlotterBase
    //{
    //    private ColumnSeries serie = new ColumnSeries();

    //    public DataPlotterSimpleBar(PlotModel model) : base(model)
    //    {
    //        model.Series.Add(serie);
    //    }

    //    public override void AddPoint(double point)
    //    {
    //        serie.Items.Add(new ColumnItem(point));
    //        if (serie.Items.Count > pointAmount)
    //        {
    //            serie.Items.RemoveAt(0);
    //        }

    //        refreshEvery++;

    //        if (refreshEvery > 5)
    //        {
    //            refreshEvery = 0;
    //            model.InvalidatePlot(true);

    //        }

            
    //    }
    //}
}
