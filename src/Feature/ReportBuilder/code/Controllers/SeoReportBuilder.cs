using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Hackthon.Foundation.Common;

namespace Hackthon.Feature.ReportBuilder.Controllers
{
    public class SeoReportBuilder
    {

        public void process()
        {
            //TODO Map PileLine Arugments for error model
            var input = new List<ValidationErrors>();

            List<DataTable> splitDt = SplitBasedOnCacheName(GetDummyDataset());
            StringBuilder sbControlHtml = new StringBuilder();
            sbControlHtml.Append(GetReportBriefInformation("Item Name"));
            sbControlHtml = BuildReport(splitDt, sbControlHtml);
            sbControlHtml.Append("<script>alert('Loading Completed');</script>");

            //Return the Resonse to the pipeline to Rednering UI
            return sbControlHtml;
        }

       
        privatestring GetAcordingTitle(string displayText)
        {
            return string.Format("<button class=\"accordion\"><b>Cache Name:</b> {0}</button>", displayText);
        }
        privatestring GetAccordingBody(string displayText)
        {
            return string.Format("<div class=\"panel\"><p>{0}</p></div>", displayText);
        }

        privatestring GetReportBriefInformation(string name)
        {
            return string.Format("<div><h3><b>Report Generated For: </b> {0}</h3></div>", name);
        }
        privateList<DataTable> SplitBasedOnCacheName(List<ValidationErrors> dt)
        {
            var dataTableList = new List<DataTable>();

            if (dt == null || dt.Count == 0)
                return dataTableList;
            try
            {
                var groupedDt = dt
                 .GroupBy((s) => s.Section)
                    .Select(@group => new
                    {
                        SectionName = group.Key,
                        Message = group.OrderByDescending(x => x.Message).FirstOrDefault(),
                        Level = group.OrderByDescending(x => x.Level).FirstOrDefault()

                    }).OrderBy((group) => group.SectionName);


                // iterate group and place each item in the group into a cloned data table
                foreach (var group in groupedDt)
                {
                    var rows = dt.Where(x => x.Section == group.SectionName).Select(r => r)
                        .ToList();
                    dataTableList.Add(ConvertToDataTable(rows, group.SectionName));

                }
            }
            catch (Exception ex)
            {
                //ErrorMessages.Add(string.Concat("Splitting the Records Based on Cache Throws Error<br/>", ex.FormatExceptionMessage()));
            }

            return dataTableList;
        }

        private  string ConvertDataTableToHTML(DataTable dt)
        {
            string html = "<table>";
            //add header row
            html += "<tr>";
            for (int i = 0; i < dt.Columns.Count; i++)
                html += "<td>" + dt.Columns[i].ColumnName + "</td>";
            html += "</tr>";
            //add rows
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                html += "<tr>";
                for (int j = 0; j < dt.Columns.Count; j++)
                    html += "<td>" + dt.Rows[i][j].ToString() + "</td>";
                html += "</tr>";
            }
            html += "</table>";
            return html;
        }

        privateStringBuilder BuildReport(List<DataTable> splitDt, StringBuilder sbControlHtml)
        {
            if (splitDt != null && splitDt.Any())
            {
                int counter = 0;
                foreach (DataTable dt in splitDt)
                {
                    counter++;
                    if (dt != null && dt.Rows != null)
                    {
                        using (StringWriter stringWriter = new StringWriter())
                        {
                            using (HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter))
                            {
                                try
                                {
                                    sbControlHtml.Append(GetAcordingTitle(dt.TableName));
                                    sbControlHtml.Append(GetAccordingBody(ConvertDataTableToHTML(dt)));
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                            }
                        }
                    }
                }

            }
            return sbControlHtml;


        }


        private  DataTable ConvertToDataTable<T>(IList<T> data, string TableName)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            table.TableName = TableName;
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }

    }
}