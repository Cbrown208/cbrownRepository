export class FancySpinner {
    Id: string;
    Show: boolean;

    constructor(show?: boolean, id?: string){
        this.Id = id;
        this.Show = show;
    }
}