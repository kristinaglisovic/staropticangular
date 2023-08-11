export interface IAddPostRequest{
    Title:string,
    TitleEng:string,
    Description:string,
    DescriptionEng:string,
    Text1:string,
    Text1Eng:string,
    Text2:string,
    Text2Eng:string,
    Heading1:string,
    Heading1Eng:string,
    Heading2:string,
    Heading2Eng:string,
    UserId:number,
    CategoriesIds:any[],
    TagsIds:any[],
    ImagesIds:any[]
}