import React from "react";
import {
  AlertDialog,
  AlertDialogAction,
  AlertDialogCancel,
  AlertDialogContent,
  AlertDialogDescription,
  AlertDialogFooter,
  AlertDialogHeader,
  AlertDialogOverlay,
  AlertDialogTitle,
  AlertDialogTrigger,
} from "./ui/alert-dialog";

interface IConfirmModal {
  title: string;
  paragraph: string;
  onContinue: () => void;
  onCancel: (bool: boolean) => void;
  isOpen: boolean;
}

const ConfirmModal = ({
  title,
  paragraph,
  onCancel,
  onContinue,
  isOpen,
}: IConfirmModal) => {
  return (
    <div>
      <AlertDialog onOpenChange={onCancel} open={isOpen}>
        <AlertDialogContent>
          <AlertDialogHeader>
            <AlertDialogTitle>{title}</AlertDialogTitle>
            <AlertDialogDescription>{paragraph}</AlertDialogDescription>
          </AlertDialogHeader>
          <AlertDialogFooter>
            <AlertDialogCancel onClick={() => onCancel(false)}>
              Cancel
            </AlertDialogCancel>
            <AlertDialogAction onClick={onContinue}>Continue</AlertDialogAction>
          </AlertDialogFooter>
        </AlertDialogContent>
      </AlertDialog>
    </div>
  );
};

export default ConfirmModal;
