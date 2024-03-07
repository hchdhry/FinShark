import axios from 'axios';
import { handlError } from '../../Handlers/ErrorHandlers';
import { UserProfileToken } from '../Models/User';


export const api = "http//localhost:5257/api";

export const loginApi = async (username:string,password:string) =>
{
    try {
       const data = await axios.post<UserProfileToken>(api + "account/login",
    {
        username:username,
        password:password
    })
        return data
    } catch (error)
    {
     handlError(error)
    }
} 
export const RegisterApi = async (email:string,username: string, password: string) => {
    try {
        const data = await axios.post<UserProfileToken>(api + "account/register",
            {   
                email:email,
                username: username,
                password: password
            })
        return data
    } catch (error) {
        handlError(error)
    }
} 