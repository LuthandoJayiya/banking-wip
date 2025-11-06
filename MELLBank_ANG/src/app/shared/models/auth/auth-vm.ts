export interface LoginVM {
  userName: string;
  password: string;
}

export interface RegistrationVM {
  userName: string;
  email: string;
  password: string;
  confirmPassword: string;
  firstName: string;
  lastName: string;
  role: string;
}

export interface CurrentUserVM {
  token: string;
  expiration: string;
  firstName: string;
  lastName: string;
  userName: string;
  roles: string[];
}

export interface UserRegisteredVM {
  userName: string;
}

export interface ResponseMessageVM {
  message: string;
}

