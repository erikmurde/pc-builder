import { AxiosInstance } from 'axios';
import { IErrorResponse } from '../dto/IErrorResponse';
import baseAxios from './baseAxios';

export abstract class BaseService {
    protected axios: AxiosInstance;

    constructor() {
        this.axios = baseAxios;
    }

    protected logError(error: any) {
        let errorData = error.response?.data as IErrorResponse;

        if (errorData.errorMessage) {
            console.log('error!\nstatus code: ', errorData.status, '\nmessage: ', errorData.errorMessage);
        } else {
            console.log('error: ', (error as Error).message);
        }
    }
}
