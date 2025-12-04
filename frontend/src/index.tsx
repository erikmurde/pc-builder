import 'jquery';
import 'popper.js';
import 'bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css'; 
import 'font-awesome/css/font-awesome.min.css';
import '@fortawesome/fontawesome-free/css/all.min.css';
import 'jquery/dist/jquery.min.js';
import 'bootstrap/dist/js/bootstrap.bundle.min.js';
import './site.css';

import {
  BrowserRouter,
  createBrowserRouter,
  RouterProvider,
} from "react-router-dom";

import { StrictMode } from 'react';
import ReactDOM from 'react-dom/client';
import Root from './routes/root';
import ErrorPage from './routes/errorPage';
import Login from './routes/identity/login';
import Home from './routes/home';
import Register from './routes/identity/register';
import CartPcs from './routes/cart/Cart';
import Profile from './routes/identity/profile/Profile';
import Panel from './routes/adminPanel/panel';
import Categories from './routes/adminPanel/categories/categories';
import CategoryEdit from './routes/adminPanel/categories/categoryEdit';
import CategoryCreate from './routes/adminPanel/categories/categoryCreate';
import Discounts from './routes/adminPanel/discounts/discounts';
import DiscountEdit from './routes/adminPanel/discounts/discountEdit';
import DiscountCreate from './routes/adminPanel/discounts/discountCreate';
import Manufacturers from './routes/adminPanel/manufacturers/manufacturers';
import ManufacturerEdit from './routes/adminPanel/manufacturers/manufacturerEdit';
import ManufacturerCreate from './routes/adminPanel/manufacturers/manufacturerCreate';
import PcBuilds from './routes/adminPanel/pcBuilds/pcBuilds';
import PcBuildEdit from './routes/adminPanel/pcBuilds/pcBuildEdit';
import PcBuildCreate from './routes/adminPanel/pcBuilds/pcBuildCreate';
import Components from './routes/adminPanel/components/components';
import ComponentEdit from './routes/adminPanel/components/componentEdit';
import ComponentCreate from './routes/adminPanel/components/componentCreate';
import ComponentAttributeCreate from './routes/adminPanel/componentAttributes/componentAttributeCreate';
import ComponentAttributeEdit from './routes/adminPanel/componentAttributes/componentAttributeEdit';
import Orders from './routes/adminPanel/orders/orders';
import OrderEdit from './routes/adminPanel/orders/orderEdit';
import Payments from './routes/adminPanel/payments/payments';
import PaymentEdit from './routes/adminPanel/payments/paymentEdit';
import ShippingCosts from './routes/adminPanel/shippingCosts/shippingCosts';
import ShippingCostEdit from './routes/adminPanel/shippingCosts/shippingCostEdit';
import ShippingCostCreate from './routes/adminPanel/shippingCosts/shippingCostCreate';
import Attributes from './routes/adminPanel/attributes/attributes';
import AttributeEdit from './routes/adminPanel/attributes/attributeEdit';
import AttributeCreate from './routes/adminPanel/attributes/attributeCreate';
import Statuses from './routes/adminPanel/statuses/statuses';
import StatusEdit from './routes/adminPanel/statuses/statusEdit';
import StatusCreate from './routes/adminPanel/statuses/statusCreate';
import ShippingMethods from './routes/adminPanel/shippingMethods/shippingMethods';
import ShippingMethodEdit from './routes/adminPanel/shippingMethods/shippingMethodEdit';
import ShippingMethodCreate from './routes/adminPanel/shippingMethods/shippingMethodCreate';
import PackageSizes from './routes/adminPanel/packageSizes/packageSizes';
import PackageSizeEdit from './routes/adminPanel/packageSizes/packageSizeEdit';
import PackageSizeCreate from './routes/adminPanel/packageSizes/packageSizeCreate';
import PcTemplates from './routes/TemplateSelect';
import PcConfigurator from './routes/configurator/PcConfigurator';
import Checkout from './routes/checkout/Checkout';
import General from './routes/identity/profile/General';
import OrderHistory from './routes/identity/profile/OrderHistory';
import PaymentHistory from './routes/identity/profile/PaymentHistory';
import StorePage from './routes/store/StorePage';
import ReviewCreate from './routes/review/ReviewCreate';
import ReviewEdit from './routes/review/ReviewEdit';
import StorePcDetails from './routes/store/StorePcDetails';
import Reviews from './routes/identity/profile/Reviews';

const router = createBrowserRouter([
  {
    path: "/",
    element: <Root />,
    errorElement: <ErrorPage />,
    children: [
      {
        path: "", 
        element: <Home />
      },
      {
        path: "home/",
        element: <Home />
      },
      {
        path: "profile/",
        element: <Profile />,
        children: [
          {
            path: "general/",
            element: <General />
          },
          {
            path: "orders/:id?",
            element: <OrderHistory />
          },
          {
            path: "payments/:id?",
            element: <PaymentHistory />
          },
          {
            path: "reviews",
            element: <Reviews />
          }
        ]
      },
      {
        path: "login/",
        element: <Login />
      },
      {
        path: "register/",
        element: <Register />
      },
      {
        path: "panel/",
        element: <Panel />,
        children: [
          {
            path: "orders/:id?",
            element: <Orders />,
          },  
          {
            path: "orders/edit/:id",
            element: <OrderEdit />,
          },  
          {
            path: "payments/:id?",
            element: <Payments />,
          },  
          {
            path: "payments/edit/:id",
            element: <PaymentEdit />,
          },  
          {
            path: "pcBuilds/:id?",
            element: <PcBuilds />,
          },  
          {
            path: "pcBuilds/edit/:id",
            element: <PcBuildEdit />,
          },  
          {
            path: "pcBuilds/create",
            element: <PcBuildCreate />,
          },  
          {
            path: "components/:id?",
            element: <Components />,
          },  
          {
            path: "components/edit/:id",
            element: <ComponentEdit />,
          },  
          {
            path: "components/create",
            element: <ComponentCreate />,
          },  
          {
            path: "componentAttributes/create/:componentId",
            element: <ComponentAttributeCreate />,
          },  
          {
            path: "componentAttributes/edit/:id",
            element: <ComponentAttributeEdit />,
          },  
          {
            path: "shippingCosts/:id?",
            element: <ShippingCosts />,
          },  
          {
            path: "shippingCosts/edit/:id",
            element: <ShippingCostEdit />,
          },  
          {
            path: "shippingCosts/create",
            element: <ShippingCostCreate />,
          },  
          {
            path: "categories/:id?",
            element: <Categories />,
          },        
          {
            path: "categories/edit/:id",
            element: <CategoryEdit />
          },
          {
            path: "categories/create",
            element: <CategoryCreate />
          },
          {
            path: "discounts/:id?",
            element: <Discounts />,
          },        
          {
            path: "discounts/edit/:id",
            element: <DiscountEdit />
          },
          {
            path: "discounts/create",
            element: <DiscountCreate />
          },
          {
            path: "attributes/:id?",
            element: <Attributes />
          },
          {
            path: "attributes/edit/:id",
            element: <AttributeEdit />
          },
          {
            path: "attributes/create?",
            element: <AttributeCreate />
          },
          {
            path: "statuses/:id?",
            element: <Statuses />
          },
          {
            path: "statuses/edit/:id",
            element: <StatusEdit />
          },
          {
            path: "statuses/create",
            element: <StatusCreate />
          },
          {
            path: "manufacturers/:id?",
            element: <Manufacturers />,
          },        
          {
            path: "manufacturers/edit/:id",
            element: <ManufacturerEdit />
          },
          {
            path: "manufacturers/create",
            element: <ManufacturerCreate />
          },
          {
            path: "shippingMethods/:id?",
            element: <ShippingMethods />
          },
          {
            path: "shippingMethods/edit/:id",
            element: <ShippingMethodEdit />
          },
          {
            path: "shippingMethods/create",
            element: <ShippingMethodCreate />
          },
          {
            path: "packageSizes/:id?",
            element: <PackageSizes />
          },
          {
            path: "packageSizes/edit/:id",
            element: <PackageSizeEdit />
          },
          {
            path: "packageSizes/create",
            element: <PackageSizeCreate />
          },
        ]
      },
      {
        path: "cart",
        element: <CartPcs />
      },
      {
        path: "checkout/",
        element: <Checkout />
      },
      {
        path: "templates/",
        element: <PcTemplates />
      },
      {
        path: "configurator/:id",
        element: <PcConfigurator />
      },
      {
        path: "prebuilt-pcs/",
        element: <StorePage />
      },
      {
        path: "prebuilt-pcs/:id",
        element: <StorePcDetails />
      },
      {
        path: "review/:id",
        element: <ReviewCreate />
      },
      {
        path: "review/edit/:id",
        element: <ReviewEdit />
      }
    ]
  }
], { basename: process.env.PUBLIC_URL });

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);

root.render(
  <StrictMode>
      <RouterProvider router={router} />
  </StrictMode>
);