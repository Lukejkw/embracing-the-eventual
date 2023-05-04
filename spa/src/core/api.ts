// We make all requests to the cart
// In production we'd want a BFF/Gateway sitting in front of all services

// We're just keeping it simple here for demo purposed

const cartBaseUrl = 'https://localhost:9001';

const baseHeaders = {
  Accept: 'application/json',
  'Content-Type': 'application/json',
};

const rejectIfUnsuccessful = (response: Response) => {
  if (!response.ok) {
    throw new Error('Something wrong');
  }
  return response;
};

export const patch = <TBody>(path: string, body: TBody) =>
  fetch(`${cartBaseUrl}/${path}`, {
    method: 'PATCH',
    headers: baseHeaders,
    body: JSON.stringify(body),
  }).then(rejectIfUnsuccessful);

export const post = <TBody>(path: string, body: TBody, headerOverrides?: HeadersInit) =>
  fetch(`${cartBaseUrl}/${path}`, {
    method: 'POST',
    headers: Object.assign(baseHeaders, headerOverrides),
    body: JSON.stringify(body),
  }).then(rejectIfUnsuccessful);

export const get = <TResponse>(path: string) =>
  fetch(`${cartBaseUrl}/${path}`, {
    method: 'GET',
    headers: {
      Accept: 'application/json',
      'Content-Type': 'application/json',
    },
  }).then((response) => response.json() as TResponse);
