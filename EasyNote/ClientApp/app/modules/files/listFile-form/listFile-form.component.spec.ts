import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListFileFormComponent } from './listFile-form.component';



describe('AddingFileFormComponent', () => {
  let component: ListFileFormComponent;
  let fixture: ComponentFixture<ListFileFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListFileFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListFileFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
