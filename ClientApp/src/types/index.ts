import { FC, LazyExoticComponent } from 'react';

export type ViewComponentProps = LazyExoticComponent<FC>;
export type RouteComponent = ViewComponentProps | FC;
