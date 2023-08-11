
export interface User{
    id:number,
    firstName:string,
    lastName:string,
    username:string,
    email:string,
    image:string,
    postsCount: number,
    createdAt:Date,
    updatedAt:Date,
    role:string,
    roleId:number;
    imageId:number;
}