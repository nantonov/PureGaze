import { httpClient } from "./httpClient";
import type { AxiosError } from "axios";

export class ApiError extends Error {
  status?: number;
  data?: unknown;

  constructor(message: string, status?: number, data?: unknown) {
    super(message);
    this.name = "ApiError";
    this.status = status;
    this.data = data;
  }
}

export abstract class BaseApi {
  protected async get<T>(url: string): Promise<T> {
    try {
      const response = await httpClient.get<T>(url);
      return response.data;
    } catch (error) {
      throw this.handleError(error);
    }
  }

  protected async post<T>(url: string, data?: unknown): Promise<T> {
    try {
      const response = await httpClient.post<T>(url, data);
      return response.data;
    } catch (error) {
      throw this.handleError(error);
    }
  }

  protected async put<T>(url: string, data?: unknown): Promise<T> {
    try {
      const response = await httpClient.put<T>(url, data);
      return response.data;
    } catch (error) {
      throw this.handleError(error);
    }
  }

  protected async delete<T>(url: string): Promise<T> {
    try {
      const response = await httpClient.delete<T>(url);
      return response.data;
    } catch (error) {
      throw this.handleError(error);
    }
  }

  private handleError(error: unknown): ApiError {
    if (error && typeof error === "object" && "response" in error) {
      const axiosError = error as AxiosError;
      return new ApiError(
        axiosError.message || "API request failed",
        axiosError.response?.status,
        axiosError.response?.data,
      );
    }
    if (error instanceof Error) {
      return new ApiError(error.message);
    }
    return new ApiError("Unknown error occurred");
  }
}
