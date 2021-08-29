
export class Column {

  public Data: string;
  public Header: string;
  public FilterParameter: string

  constructor(Data: string, Header: string="", FilterParametr: string="") {
    this.Data = Data;
    this.Header = Header;
    this.FilterParameter = FilterParametr;
  }


}
