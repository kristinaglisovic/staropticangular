export interface Post{
    id:number,
    title:string,
    description:string,
    author:string,
    isActive:boolean,
    createdAt:Date,
    updatedAt:Date,
    commentsCount:number,
    authorUsername:string,
    likes:number;
    images:string;
    categories:string[];
    likesInfo:any[];
}