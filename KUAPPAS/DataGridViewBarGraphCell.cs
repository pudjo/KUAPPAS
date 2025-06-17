// Add these to your file:
using System.Drawing;
using System.Windows.Forms;

// Your class should look like this:
public class DataGridViewBarGraphCell :
  DataGridViewTextBoxCell
{
    protected override void Paint(
      Graphics graphics,
      Rectangle clipBounds,
      Rectangle cellBounds,
      int rowIndex,
      DataGridViewElementStates cellState,
      object value, object formattedValue,
      string errorText,
      DataGridViewCellStyle cellStyle,
      DataGridViewAdvancedBorderStyle
      advancedBorderStyle,
      DataGridViewPaintParts paintParts)
    {
        //  Get the value of the cell:
        //decimal cellValue = 0;
        //if (Convert.IsDBNull(value))
        //{
        //    cellValue = 0;
        //}
        //else
        //{
        //    cellValue = Convert.ToDecimal(value);
        //}

        //  If cell value is 0, you still
        //  want to show something, so set the value
        //  to 1.
        //if (cellValue == 0)
        //{
        //    cellValue = 1;
        //}

        base.Paint(graphics, clipBounds,
  cellBounds, rowIndex, cellState,
  value, "", errorText,
  cellStyle, advancedBorderStyle,
  paintParts);
    }

}