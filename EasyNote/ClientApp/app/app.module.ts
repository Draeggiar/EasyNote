import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeModule } from './modules/home/home.module';
import { NavmenuComponent } from './components/navmenu/navmenu.component';
import { ConfigService } from './services/config.service';
import { FilesListComponent } from './components/files-list/files-list.component';

@NgModule({
  declarations: [
    AppComponent,
    NavmenuComponent,
    FilesListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HomeModule,
    HttpModule,
  ],
  providers: [ConfigService],
  bootstrap: [AppComponent]
})
export class AppModule { }
