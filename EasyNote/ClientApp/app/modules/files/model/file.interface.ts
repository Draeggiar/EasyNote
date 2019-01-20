export interface File {
  id: number;
  name: string;
  author: string;
  content: string;
  modifiedBy: string;
  isNew: boolean;
  isCheckouted: boolean;
}
