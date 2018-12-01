import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
@Component({
  selector: 'nav-menu',
  templateUrl: './navmenu.component.html',
  styleUrls: ['./navmenu.component.scss']
})



export class NavmenuComponent implements OnInit {

  public files: FileController[];
  public cacheFiles: FileController[];
  public summaries: any[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<FileController[]>(baseUrl + 'API/FileController').subscribe(result => {
      this.files = result;
      this.cacheFiles = result;
    }, error => console.error(error));

    //http.get<any[]>(baseUrl + 'api/SampleData/GetSummaries').subscribe(result => {
    //  this.summaries = result;
   // }, error => console.error(error));
  }

  //filterForeCasts(filterVal: any) {
   // if (filterVal == "0")
   //   this.files = this.cacheFiles;
   // else
   //   this.files = this.cacheFiles.filter((item) => item.summary == filterVal);
  //}



  ngOnInit() {
  }

}
interface FileController {
  name: string;
  author: number;
  content: number;
}

interface Summary {
  name: string;
}
