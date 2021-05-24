import React, { useState } from "react";
import { observer } from "mobx-react-lite";
import { Button, Card, Grid, Header, Segment, Tab } from "semantic-ui-react";
import { UserProfile } from "../../app/models/profile";
import { useStore } from "../../app/stores/store";
import { UserDescriptionFormValues } from "../../app/models/userDescription";
import { Form, Formik } from "formik";
import MyTextArea from "../../app/common/form/MyTextArea";
import { Link, useParams } from "react-router-dom";
import * as Yup from "yup";

interface Props {
    userProfile: UserProfile;
}

export default observer(function UserProfileAbout({userProfile}: Props) {
    const {userName} = useParams<{ userName: string }>();
    const {
        userProfileStore: {
            isCurrentUser,
            uploadPhoto,
            uploading,
            createDescription,
            uploadDescription,
            setMainPhoto,
            loading,
            deletePhoto, loadUserProfile
        }
    } = useStore();

    const validationSchema = Yup.object({
        description: Yup.string().required('The description field is required'),
    })

    const [addPhotoMode, setAddPhotoMode] = useState(false);
    const [target, setTraget] = useState('');

    function handleFormSubmit(description: UserDescriptionFormValues) {
        if (userProfile.userDescription === null) {
            createDescription(description)
                .then(() => setAddPhotoMode(false))
                .then(() => loadUserProfile(userName));
        } else {
            uploadDescription(description)
                .then(() => setAddPhotoMode(false))
                .then(() => loadUserProfile(userName));
        }
    }

    return (
        <Tab.Pane>
            <Grid>
                <Grid.Column width={16}>
                    <Header floated='left' icon='info circle' content='Description'/>
                    {isCurrentUser && (
                        <Button floated='right' basic content={addPhotoMode ? 'Cancel' : 'Edit description'}
                                onClick={() => setAddPhotoMode(!addPhotoMode)}
                        />
                    )}
                </Grid.Column>
                <Grid.Column width={16}>
                    {addPhotoMode ? (
                        <Segment clearing>
                            <Formik
                                validationSchema={validationSchema}
                                enableReinitialize
                                initialValues={new UserDescriptionFormValues(userProfile.userDescription)}
                                onSubmit={(values, {setErrors}) => handleFormSubmit(values)}>
                                {({handleSubmit, isValid, isSubmitting, dirty, errors}) => (
                                    <Form className='ui form' onSubmit={handleSubmit} autoComplete='off'>
                                        <MyTextArea rows={5} placeholder='Description'
                                                    name='description'/>
                                        <Button
                                            disabled={isSubmitting || !dirty || !isValid}
                                            loading={isSubmitting} floated='right' positive type='submit'
                                            content='Submit'/>
                                    </Form>
                                )}
                            </Formik>
                        </Segment>
                    ) : (
                        <Card.Group>
                            <Card fluid key={userProfile.displayName}>
                                {userProfile.userDescription}
                            </Card>
                        </Card.Group>
                    )}
                </Grid.Column>
            </Grid>

        </Tab.Pane>
    )
})