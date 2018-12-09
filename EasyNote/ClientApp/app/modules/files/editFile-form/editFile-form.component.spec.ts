import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddingFileFormComponent } from './addingFile-form.component';



describe('AddingFileFormComponent', () => {
  let component: AddingFileFormComponent;
  let fixture: ComponentFixture<AddingFileFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddingFileFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddingFileFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
